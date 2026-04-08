namespace WordReplacement.Models;

public class ReplacementStatistics
{
    public string InputFilePath { get; set; } = string.Empty;
    public string OutputFilePath { get; set; } = string.Empty;
    public string SearchWord { get; set; } = string.Empty;
    public string ReplacementWord { get; set; } = string.Empty;
    public int MatchesFound { get; set; }
    public int ReplacementsMade { get; set; }
    public long OriginalFileSizeBytes { get; set; }
    public long UpdatedFileSizeBytes { get; set; }
    public long ExecutionTimeMilliseconds { get; set; }
}