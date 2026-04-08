namespace TextModerator.Models;

public class ModerationResult
{
    public string InputFilePath { get; set; } = string.Empty;
    public string ForbiddenWordsFilePath { get; set; } = string.Empty;
    public string OutputFilePath { get; set; } = string.Empty;
    public int ForbiddenWordsCount { get; set; }
    public int TotalReplacements { get; set; }
    public string ModeratedText { get; set; } = string.Empty;
}