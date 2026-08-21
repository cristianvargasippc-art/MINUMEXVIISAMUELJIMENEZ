namespace Delegame.Models;

public sealed class GameSession
{
    private readonly List<Question> _questions;

    public GameSession(Workshop workshop, Difficulty difficulty, IEnumerable<Question> questions)
    {
        Workshop = workshop;
        Difficulty = difficulty;
        _questions = [.. questions];
        SecondsMax = difficulty.Seconds();
        SecondsLeft = SecondsMax;
    }

    public Workshop Workshop { get; }
    public Difficulty Difficulty { get; }
    public IReadOnlyList<Question> Questions => _questions;

    public int Index { get; private set; }
    public int Score { get; private set; }
    public int Correct { get; private set; }
    public int Wrong { get; private set; }
    public int Streak { get; private set; }
    public double Multiplier { get; private set; } = 1.0;

    public bool Answered { get; private set; }
    public int? PickedIndex { get; private set; }
    public bool LastAnswerCorrect { get; private set; }
    public bool TimedOut { get; private set; }
    public int LastPoints { get; private set; }

    public int SecondsMax { get; }
    public int SecondsLeft { get; private set; }

    public Question Current => _questions[Index];
    public int Total => _questions.Count;
    public bool IsLastQuestion => Index >= Total - 1;
    public double Progress => Total == 0 ? 0 : (double)Index / Total;
    public int Altitude => (int)Math.Round(Progress * 35000);
    public int Accuracy => Total == 0 ? 0 : (int)Math.Round(Correct / (double)Total * 100);

    public bool Tick()
    {
        if (Answered || SecondsLeft <= 0) return false;
        SecondsLeft--;
        if (SecondsLeft > 0) return true;
        Timeout();
        return true;
    }

    public int Answer(int optionIndex)
    {
        if (Answered) return 0;
        Answered = true;
        PickedIndex = optionIndex;
        LastAnswerCorrect = optionIndex == Current.Correct;

        if (!LastAnswerCorrect)
        {
            RegisterMiss();
            return 0;
        }

        var bonus = (int)Math.Round(SecondsLeft / (double)SecondsMax * 60);
        LastPoints = (int)Math.Round((100 + bonus) * Multiplier);
        Score += LastPoints;
        Correct++;
        Streak = Math.Min(Streak + 1, 99);
        Multiplier = Math.Min(3.0, 1.0 + Streak * 0.25);
        return LastPoints;
    }

    public void Timeout()
    {
        if (Answered) return;
        Answered = true;
        TimedOut = true;
        LastAnswerCorrect = false;
        RegisterMiss();
    }

    public bool Advance()
    {
        if (IsLastQuestion) return false;
        Index++;
        ResetQuestionState();
        return true;
    }

    private void RegisterMiss()
    {
        Wrong++;
        Streak = 0;
        Multiplier = Math.Max(1.0, Multiplier - 0.2);
        LastPoints = 0;
    }

    private void ResetQuestionState()
    {
        Answered = false;
        PickedIndex = null;
        TimedOut = false;
        LastAnswerCorrect = false;
        LastPoints = 0;
        SecondsLeft = SecondsMax;
    }
}
