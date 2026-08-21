using Delegame.Models;

namespace Delegame.Services;

public sealed class GameFlow : IDisposable
{
    private readonly WorkshopService _workshops;
    private readonly RoomService _rooms;
    private readonly AvatarCatalog _avatars;
    private readonly UserService _users;
    private readonly ToastService _toasts;

    private CancellationTokenSource? _timerCancel;
    private Room? _room;
    private bool _starting;

    public GameFlow(
        WorkshopService workshops,
        RoomService rooms,
        AvatarCatalog avatars,
        UserService users,
        ToastService toasts)
    {
        _workshops = workshops;
        _rooms = rooms;
        _avatars = avatars;
        _users = users;
        _toasts = toasts;

        Player.AvatarKey = _avatars.Default.Key;
        Player.AvatarUri = _avatars.Default.Uri;
        Player.AvatarName = _avatars.Default.Name;
    }

    public event Func<Task>? Changed;

    public Screen Screen { get; private set; } = Screen.Home;
    public GameMode Mode { get; private set; } = GameMode.Solo;
    public PlayerProfile Player { get; } = new();
    public GameSession? Game { get; private set; }
    public AppUser? Admin { get; private set; }

    public string PlayerId { get; private set; } = string.Empty;
    public int HostMaxPlayers { get; set; } = 30;

    public bool ShowCountdown { get; private set; }
    public int CountdownValue { get; private set; }
    public bool ShowPlane { get; private set; }
    public bool ShowArrival { get; private set; }
    public int FloatingPoints { get; private set; }

    public Room? Room
    {
        get => _room;
        private set
        {
            if (_room is not null) _room.Changed -= OnRoomChanged;
            _room = value;
            if (_room is not null) _room.Changed += OnRoomChanged;
        }
    }

    public bool IsMultiplayer => Room is not null;
    public bool IsAdmin => Admin is not null;
    public bool IsMasterAdmin => Admin?.IsMaster == true;

    public async Task Go(Screen screen)
    {
        Screen = screen;
        await Notify();
    }

    public async Task SelectMode(GameMode mode)
    {
        Mode = mode;
        Screen = mode == GameMode.Player ? Screen.Join : Screen.Register;
        await Notify();
    }

    public void SetAvatar(Avatar avatar)
    {
        Player.AvatarKey = avatar.Key;
        Player.AvatarUri = avatar.Uri;
        Player.AvatarName = avatar.Name;
    }

    public async Task Register()
    {
        if (string.IsNullOrWhiteSpace(Player.Name))
        {
            _toasts.Warn("Ingresa tu nombre de delegado.");
            return;
        }

        if (string.IsNullOrWhiteSpace(Player.Country))
        {
            _toasts.Warn("Escribe tu país o institución.");
            return;
        }

        Player.Name = Player.Name.Trim();
        Player.Country = Player.Country.Trim();
        if (string.IsNullOrWhiteSpace(Player.Title)) Player.Title = "Delegado";

        _toasts.Success($"Bienvenido a bordo, {Player.Name}");

        if (Mode == GameMode.Host) await CreateRoom();
        else await Go(Screen.Modules);
    }

    public async Task CreateRoom()
    {
        Room = _rooms.Create(Player, HostMaxPlayers);
        PlayerId = Room.HostId;
        Mode = GameMode.Host;
        await Go(Screen.HostLobby);
    }

    public async Task JoinRoom(string code)
    {
        if (string.IsNullOrWhiteSpace(Player.Name) || string.IsNullOrWhiteSpace(Player.Country) || string.IsNullOrWhiteSpace(code))
        {
            _toasts.Warn("Completa todos los campos.");
            return;
        }

        if (code.Trim().Length != 6)
        {
            _toasts.Warn("El código debe tener 6 caracteres.");
            return;
        }

        Player.Name = Player.Name.Trim();
        Player.Country = Player.Country.Trim();

        var result = _rooms.Join(code, Player);
        if (!result.Success)
        {
            _toasts.Error(result.Message, 5000);
            return;
        }

        Room = result.Room;
        PlayerId = result.Player!.Id;
        Mode = GameMode.Player;
        await Go(Screen.PlayerLobby);
    }

    public void SetRoomCapacity(int max)
    {
        HostMaxPlayers = Math.Clamp(max, 2, 500);
        if (Room is null) return;
        Room.MaxPlayers = HostMaxPlayers;
        Room.Touch();
    }

    public async Task LeaveRoom()
    {
        if (Room is not null)
        {
            if (PlayerId == Room.HostId) _rooms.Close(Room);
            else _rooms.Leave(Room, PlayerId);
        }

        Room = null;
        PlayerId = string.Empty;
        Mode = GameMode.Solo;
        await Go(Screen.Home);
    }

    public async Task HostLaunch(string moduleKey)
    {
        if (Room is null)
        {
            await StartSolo(moduleKey);
            return;
        }

        _rooms.Launch(Room, moduleKey);
        await StartGame(moduleKey);
    }

    public async Task StartSolo(string moduleKey)
    {
        Mode = GameMode.Solo;
        await StartGame(moduleKey);
    }

    public async Task Retry()
    {
        var key = Game?.Workshop.Key;
        if (Mode == GameMode.Solo && key is not null) await StartSolo(key);
        else await Go(Screen.Modules);
    }

    public async Task Pick(int index)
    {
        if (Game is null || Game.Answered) return;

        CancelTimer();
        var points = Game.Answer(index);

        if (points > 0)
        {
            FloatingPoints = points;
            _toasts.Success($"+{points} pts · ×{Game.Multiplier:0.0}", 1800);
            SyncScore();
        }

        await FlashPlane();
    }

    public async Task Next()
    {
        if (Game is null) return;

        if (Game.Advance())
        {
            StartTimer();
            await Notify();
            return;
        }

        CancelTimer();
        if (Room is not null) _rooms.MarkFinished(Room, PlayerId);

        ShowArrival = true;
        await Notify();
    }

    public async Task CloseArrival(bool showResults)
    {
        ShowArrival = false;
        await Go(showResults ? Screen.Results : Screen.Home);
    }

    public AuthResult AdminLogin(string name, string password)
    {
        var result = _users.Authenticate(name.Trim(), password);
        if (result.Success)
        {
            Admin = result.User;
            _toasts.Success(result.Message, 3500);
        }
        else
        {
            _toasts.Error(result.Message, 5000);
        }

        return result;
    }

    public async Task AdminLogout()
    {
        Admin = null;
        _toasts.Info("Sesión cerrada", 2000);
        await Go(Screen.Home);
    }

    private async Task StartGame(string moduleKey)
    {
        if (_starting) return;

        var workshop = _workshops.Find(moduleKey);
        if (workshop is null || workshop.Questions.Count == 0)
        {
            _toasts.Error("Módulo no disponible.");
            return;
        }

        _starting = true;

        try
        {
            Game = new GameSession(workshop, Player.Difficulty, Shuffle(workshop.Questions));
            ShowArrival = false;

            if (Room is not null) _rooms.UpdateScore(Room, PlayerId, 0, 0);

            await RunCountdown();

            Screen = Screen.Game;
            StartTimer();
            await FlashPlane();
        }
        finally
        {
            _starting = false;
        }
    }

    private async Task RunCountdown()
    {
        ShowCountdown = true;

        for (var n = 3; n > 0; n--)
        {
            CountdownValue = n;
            await Notify();
            await Task.Delay(950);
        }

        ShowCountdown = false;
        await Notify();
    }

    private async Task FlashPlane()
    {
        ShowPlane = true;
        await Notify();

        await Task.Delay(2000);
        ShowPlane = false;
        FloatingPoints = 0;
        await Notify();
    }

    private void StartTimer()
    {
        CancelTimer();
        _timerCancel = new CancellationTokenSource();
        _ = RunTimer(_timerCancel.Token);
    }

    private void CancelTimer()
    {
        _timerCancel?.Cancel();
        _timerCancel?.Dispose();
        _timerCancel = null;
    }

    private async Task RunTimer(CancellationToken token)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        try
        {
            while (await timer.WaitForNextTickAsync(token))
            {
                if (Game is null || !Game.Tick()) return;

                if (Game.Answered)
                {
                    _toasts.Warn("Tiempo agotado — siguiente pregunta", 2000);
                    await FlashPlane();
                    return;
                }

                await Notify();
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    private void SyncScore()
    {
        if (Room is null || Game is null) return;
        _rooms.UpdateScore(Room, PlayerId, Game.Score, Game.Streak);
    }

    private void OnRoomChanged()
    {
        if (Mode == GameMode.Player
            && Screen == Screen.PlayerLobby
            && Room is { Status: RoomStatus.Started, ModuleKey: not null } started)
        {
            _ = StartGame(started.ModuleKey!);
            return;
        }

        _ = Notify();
    }

    private static List<Question> Shuffle(IEnumerable<Question> source)
    {
        var list = source.ToList();

        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = Random.Shared.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }

        return list;
    }

    private Task Notify() => Changed?.Invoke() ?? Task.CompletedTask;

    public void Dispose()
    {
        CancelTimer();
        if (Room is not null && PlayerId != Room.HostId) _rooms.Leave(Room, PlayerId);
        Room = null;
    }
}
