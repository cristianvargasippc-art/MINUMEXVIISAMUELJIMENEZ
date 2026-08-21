using System.Text.Json;
using Delegame.Models;

namespace Delegame.Services;

public sealed class StoreData
{
    public List<AppUser> Users { get; set; } = [];
    public List<Workshop> CustomWorkshops { get; set; } = [];
    public Dictionary<string, bool> WorkshopEnabled { get; set; } = [];
}

public sealed class DelegameStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly string _path;
    private readonly Lock _gate = new();
    private StoreData _data;

    public DelegameStore(IHostEnvironment environment, IConfiguration configuration, ILogger<DelegameStore> logger)
    {
        var directory = Path.Combine(environment.ContentRootPath, "App_Data");
        Directory.CreateDirectory(directory);
        _path = Path.Combine(directory, "delegame.json");
        _data = Load();
        SeedMaster(configuration, logger);
    }

    public event Action? Changed;

    public void Mutate(Action<StoreData> mutation)
    {
        lock (_gate)
        {
            mutation(_data);
            Persist();
        }

        Changed?.Invoke();
    }

    public T Read<T>(Func<StoreData, T> selector)
    {
        lock (_gate)
        {
            return selector(_data);
        }
    }

    private StoreData Load()
    {
        if (!File.Exists(_path)) return new StoreData();

        try
        {
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<StoreData>(json, SerializerOptions) ?? new StoreData();
        }
        catch (JsonException)
        {
            File.Move(_path, _path + ".corrupt-" + DateTime.UtcNow.Ticks, overwrite: true);
            return new StoreData();
        }
    }

    private void Persist()
    {
        var temp = _path + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(_data, SerializerOptions));
        File.Move(temp, _path, overwrite: true);
    }

    private void SeedMaster(IConfiguration configuration, ILogger<DelegameStore> logger)
    {
        lock (_gate)
        {
            if (_data.Users.Any(u => u.IsMaster)) return;

            var name = configuration["Delegame:MasterUser"] ?? "admin";
            var configured = configuration["Delegame:MasterPassword"];
            var temporary = string.IsNullOrWhiteSpace(configured);
            var password = temporary ? PasswordHasher.Generate(14) : configured!;

            _data.Users.Add(new AppUser
            {
                Name = name,
                Role = "admin",
                PasswordHash = PasswordHasher.Hash(password)
            });
            Persist();

            if (temporary)
            {
                logger.LogWarning(
                    "Cuenta maestra \"{User}\" creada con contraseña temporal {Password}. Configura Delegame:MasterPassword y bórrala del registro.",
                    name, password);
            }
        }
    }
}
