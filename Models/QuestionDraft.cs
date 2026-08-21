namespace Delegame.Models;

public sealed class QuestionDraft
{
    public string Text { get; set; } = string.Empty;
    public string[] Options { get; set; } = ["", "", "", ""];
    public int Correct { get; set; }
    public string Explanation { get; set; } = string.Empty;

    public bool IsComplete =>
        !string.IsNullOrWhiteSpace(Text) && Options.All(o => !string.IsNullOrWhiteSpace(o));

    public Question ToQuestion() => new()
    {
        Text = Text.Trim(),
        Options = [.. Options.Select(o => o.Trim())],
        Correct = Correct,
        Explanation = Explanation.Trim(),
        Principle = string.Empty
    };
}
