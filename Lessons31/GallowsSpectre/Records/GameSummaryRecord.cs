namespace GallowsSpectre.Records;

public record GameSummaryRecord(
    bool IsWin,
    string Word,
    string UsedLetters,
    int AttemptsUsed,
    int MaxAttempts,
    int WrongAttempts,
    string DurationText
);
