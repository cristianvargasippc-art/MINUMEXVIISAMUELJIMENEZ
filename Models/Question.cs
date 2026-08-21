namespace Delegame.Models;

public sealed class Question
{
    public string Text { get; set; } = string.Empty;
    public string? Context { get; set; }
    public List<string> Options { get; set; } = [];
    public int Correct { get; set; }
    public string Explanation { get; set; } = string.Empty;
    public string Principle { get; set; } = string.Empty;
}
