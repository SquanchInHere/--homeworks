namespace Album.Models;

public class Song
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public int DurationSeconds { get; set; }
    public string? LyricsFilePath { get; set; }

    public override string ToString()
    {
        string lyricsInfo = string.IsNullOrWhiteSpace(LyricsFilePath) ? "No lyrics file" : LyricsFilePath!;
        return $"Song ID: {Id}{Environment.NewLine}" +
               $"Title: {Title}{Environment.NewLine}" +
               $"Duration: {DurationSeconds} sec{Environment.NewLine}" +
               $"Lyrics File: {lyricsInfo}";
    }
}
