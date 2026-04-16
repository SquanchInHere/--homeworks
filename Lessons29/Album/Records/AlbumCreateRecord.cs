namespace Album.Records;

public record AlbumCreateRecord(
    string AlbumTitle,
    string ArtistName,
    int ReleaseYear,
    string StudioName,
    List<SongCreateRecord> Songs
);
