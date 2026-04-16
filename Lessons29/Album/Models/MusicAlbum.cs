namespace Album.Models;

public class MusicAlbum
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AlbumTitle { get; set; } = string.Empty;
    public string ArtistName { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string StudioName { get; set; } = string.Empty;
    public List<Song> Songs { get; set; } = new();
    public int TotalDurationSeconds => Songs.Sum(s => s.DurationSeconds);

    public override string ToString()
    {
        string songsText = Songs.Count == 0
            ? "No songs"
            : string.Join(Environment.NewLine + Environment.NewLine, Songs.Select((song, index) => $"{index + 1}. {song}"));

        return $"Album ID: {Id}{Environment.NewLine}" +
               $"Album Title: {AlbumTitle}{Environment.NewLine}" +
               $"Artist: {ArtistName}{Environment.NewLine}" +
               $"Release Year: {ReleaseYear}{Environment.NewLine}" +
               $"Studio: {StudioName}{Environment.NewLine}" +
               $"Total Duration: {TotalDurationSeconds} sec{Environment.NewLine}" +
               $"Songs:{Environment.NewLine}{songsText}";
    }
}
