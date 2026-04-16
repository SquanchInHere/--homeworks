namespace Album.Records;

public record SongCreateRecord(
    string Title,
    int DurationSeconds,
    string? LyricsFilePath
);
