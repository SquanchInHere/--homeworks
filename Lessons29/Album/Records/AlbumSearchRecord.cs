namespace Album.Records;

public record AlbumSearchRecord(
    string? AlbumTitle = null,
    string? ArtistName = null,
    int? ReleaseYear = null,
    string? StudioName = null,
    int? MaxDurationSeconds = null,
    string? SongTitle = null
);
