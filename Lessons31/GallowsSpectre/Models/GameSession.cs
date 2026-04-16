using GallowsSpectre.Enums;

namespace GallowsSpectre.Models;

public class GameSession
{
    public string Word { get; set; } = string.Empty;
    public DifficultyLevel Difficulty { get; set; }
    public HashSet<char> UsedLetters { get; } = new();
    public HashSet<char> CorrectLetters { get; } = new();
    public int WrongAttempts { get; set; }
    public int AttemptsUsed { get; set; }
    public int MaxAttempts { get; set; }
    public DateTime StartedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? FinishedAtUtc { get; set; }

    public bool IsWon => Word.All(ch => CorrectLetters.Contains(ch));
    public bool IsLost => WrongAttempts >= 6 || AttemptsUsed >= MaxAttempts;
    public bool IsFinished => IsWon || IsLost;

    public TimeSpan Duration => (FinishedAtUtc ?? DateTime.UtcNow) - StartedAtUtc;

    public string BuildMaskedWord()
    {
        return string.Join(' ', Word.Select(ch => CorrectLetters.Contains(ch) ? ch : '_'));
    }

    public string GetUsedLettersText()
    {
        return UsedLetters.Count == 0
            ? "-"
            : string.Join(", ", UsedLetters.OrderBy(ch => ch));
    }
}
