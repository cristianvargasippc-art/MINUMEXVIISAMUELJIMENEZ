namespace Delegame.Models;

public sealed class PlayerProfile
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string AvatarKey { get; set; } = "d0";
    public string AvatarUri { get; set; } = string.Empty;
    public string AvatarName { get; set; } = string.Empty;
    public Difficulty Difficulty { get; set; } = Difficulty.Adjunto;
}
