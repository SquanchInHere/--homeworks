using Album.Interfaces;
using Album.Models;

namespace Album.Services;

public class ReportService : IReportService
{
    public ReportResult GetByArtist(List<MusicAlbum> albums)
    {
        var result = new ReportResult { Title = "=== Report by Artist ===" };

        if (albums.Count == 0)
        {
            result.Lines.Add("No albums available.");
            return result;
        }

        foreach (var group in albums.GroupBy(a => a.ArtistName))
        {
            result.Lines.Add(string.Empty);
            result.Lines.Add($"Artist: {group.Key}");
            foreach (var album in group)
            {
                result.Lines.Add($"- {album.AlbumTitle} ({album.ReleaseYear})");
            }
        }

        return result;
    }

    public ReportResult GetByReleaseYear(List<MusicAlbum> albums)
    {
        var result = new ReportResult { Title = "=== Report by Release Year ===" };

        if (albums.Count == 0)
        {
            result.Lines.Add("No albums available.");
            return result;
        }

        foreach (var group in albums.OrderBy(a => a.ReleaseYear).GroupBy(a => a.ReleaseYear))
        {
            result.Lines.Add(string.Empty);
            result.Lines.Add($"Year: {group.Key}");
            foreach (var album in group)
            {
                result.Lines.Add($"- {album.AlbumTitle} / {album.ArtistName}");
            }
        }

        return result;
    }

    public ReportResult GetByStudio(List<MusicAlbum> albums)
    {
        var result = new ReportResult { Title = "=== Report by Studio ===" };

        if (albums.Count == 0)
        {
            result.Lines.Add("No albums available.");
            return result;
        }

        foreach (var group in albums.GroupBy(a => a.StudioName))
        {
            result.Lines.Add(string.Empty);
            result.Lines.Add($"Studio: {group.Key}");
            foreach (var album in group)
            {
                result.Lines.Add($"- {album.AlbumTitle} / {album.ArtistName}");
            }
        }

        return result;
    }

    public ReportResult GetByDuration(List<MusicAlbum> albums)
    {
        var result = new ReportResult { Title = "=== Report by Duration ===" };

        if (albums.Count == 0)
        {
            result.Lines.Add("No albums available.");
            return result;
        }

        foreach (var album in albums.OrderBy(a => a.TotalDurationSeconds))
        {
            result.Lines.Add($"{album.AlbumTitle} / {album.ArtistName} - {album.TotalDurationSeconds} sec");
        }

        return result;
    }
}
