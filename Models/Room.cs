using System.Collections.Concurrent;

namespace Delegame.Models;

public enum RoomStatus
{
    Waiting,
    Started,
    Finished
}

public sealed class Room
{
    public string Code { get; init; } = string.Empty;
    public string HostId { get; init; } = "HOST";
    public int MaxPlayers { get; set; } = 30;
    public string? ModuleKey { get; set; }
    public RoomStatus Status { get; set; } = RoomStatus.Waiting;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;

    public ConcurrentDictionary<string, RoomPlayer> Players { get; } = new(StringComparer.Ordinal);

    public event Action? Changed;

    public IReadOnlyList<RoomPlayer> Ordered =>
        [.. Players.Values.OrderBy(p => p.JoinedAt)];

    public IReadOnlyList<RoomPlayer> Ranked =>
        [.. Players.Values.OrderByDescending(p => p.Score).ThenBy(p => p.JoinedAt)];

    public void Touch()
    {
        LastActivity = DateTime.UtcNow;
        Changed?.Invoke();
    }
}
