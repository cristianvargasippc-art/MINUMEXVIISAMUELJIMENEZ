namespace Delegame.Models;

public sealed class RoomPlayer
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string AvatarKey { get; set; } = "d0";
    public string AvatarUri { get; set; } = string.Empty;
    public int Score { get; set; }
    public int Streak { get; set; }
    public bool IsHost { get; init; }
    public bool Finished { get; set; }
    public DateTime JoinedAt { get; init; } = DateTime.UtcNow;
}
