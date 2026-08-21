using System.Text.Json.Serialization;

namespace Delegame.Models;

public sealed class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = "delegado";
    public string PasswordHash { get; set; } = string.Empty;
    public bool Disabled { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public bool IsMaster => string.Equals(Role, "admin", StringComparison.OrdinalIgnoreCase);
}
