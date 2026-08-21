using System.Collections.Concurrent;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Delegame.Models;

namespace Delegame.Services;

public sealed record JoinResult(bool Success, string Message, Room? Room = null, RoomPlayer? Player = null);

public sealed class RoomService
{
    private const string CodeAlphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
    private static readonly TimeSpan RoomLifetime = TimeSpan.FromHours(6);

    private readonly ConcurrentDictionary<string, Room> _rooms = new(StringComparer.Ordinal);

    public Room Create(PlayerProfile host, int maxPlayers)
    {
        Purge();

        string code;
        do
        {
            code = RandomNumberGenerator.GetString(CodeAlphabet, 6);
        }
        while (!_rooms.TryAdd(code, new Room { Code = code, MaxPlayers = maxPlayers }));

        var room = _rooms[code];
        room.Players["HOST"] = new RoomPlayer
        {
            Id = "HOST",
            IsHost = true,
            Name = host.Name,
            Country = host.Country,
            AvatarKey = host.AvatarKey,
            AvatarUri = host.AvatarUri
        };

        return room;
    }

    public Room? Find(string code) =>
        _rooms.TryGetValue(code.Trim().ToUpperInvariant(), out var room) ? room : null;

    public JoinResult Join(string code, PlayerProfile profile)
    {
        var room = Find(code);
        if (room is null) return new JoinResult(false, "Sala no encontrada. Verifica el código.");

        if (room.Status != RoomStatus.Waiting)
            return new JoinResult(false, "El vuelo ya despegó. Pide otro código al piloto.");

        var id = StableId(profile.Name, profile.Country);

        if (!room.Players.ContainsKey(id) && room.Players.Count >= room.MaxPlayers)
            return new JoinResult(false, "La sala alcanzó su cupo máximo.");

        if (Conflicts(room, id, p => p.Country, profile.Country))
            return new JoinResult(false, $"El país \"{profile.Country}\" ya lo representa otra delegación. Elige otro.");

        if (Conflicts(room, id, p => p.Name, profile.Name))
            return new JoinResult(false, $"El nombre \"{profile.Name}\" ya está en uso en esta sala.");

        var player = new RoomPlayer
        {
            Id = id,
            Name = profile.Name,
            Country = profile.Country,
            AvatarKey = profile.AvatarKey,
            AvatarUri = profile.AvatarUri
        };

        room.Players[id] = player;
        room.Touch();
        return new JoinResult(true, "A bordo", room, player);
    }

    public bool CountryTaken(Room room, string playerId, string country) =>
        Conflicts(room, playerId, p => p.Country, country);

    public void Leave(Room room, string playerId)
    {
        if (playerId == room.HostId) return;
        room.Players.TryRemove(playerId, out _);
        room.Touch();
    }

    public void Close(Room room)
    {
        _rooms.TryRemove(room.Code, out _);
        room.Status = RoomStatus.Finished;
        room.Touch();
    }

    public void Launch(Room room, string moduleKey)
    {
        room.ModuleKey = moduleKey;
        room.Status = RoomStatus.Started;
        room.Touch();
    }

    public void UpdateScore(Room room, string playerId, int score, int streak)
    {
        if (!room.Players.TryGetValue(playerId, out var player)) return;
        player.Score = score;
        player.Streak = streak;
        room.Touch();
    }

    public void MarkFinished(Room room, string playerId)
    {
        if (!room.Players.TryGetValue(playerId, out var player)) return;
        player.Finished = true;
        room.Touch();
    }

    public static string StableId(string name, string country)
    {
        var raw = Normalize(name + "___" + country);
        var slug = raw.Length > 28 ? raw[..28] : raw;

        uint hash = 0;
        foreach (var c in slug) hash = hash * 31 + c;

        return slug + "_" + (hash % 9999).ToString("x", CultureInfo.InvariantCulture);
    }

    private static bool Conflicts(Room room, string playerId, Func<RoomPlayer, string> selector, string value)
    {
        var candidate = value.Trim();
        return room.Players.Any(entry =>
            entry.Key != playerId &&
            string.Equals(selector(entry.Value).Trim(), candidate, StringComparison.OrdinalIgnoreCase));
    }

    private static string Normalize(string value)
    {
        var decomposed = value.ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(decomposed.Length);

        foreach (var c in decomposed)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark) continue;
            builder.Append(char.IsAsciiLetterOrDigit(c) ? c : 'x');
        }

        return builder.ToString();
    }

    private void Purge()
    {
        var cutoff = DateTime.UtcNow - RoomLifetime;
        foreach (var entry in _rooms.Where(r => r.Value.LastActivity < cutoff))
            _rooms.TryRemove(entry.Key, out _);
    }
}
