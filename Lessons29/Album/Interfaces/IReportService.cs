using Album.Models;

namespace Album.Interfaces;

public interface IReportService
{
    ReportResult GetByArtist(List<MusicAlbum> albums);
    ReportResult GetByReleaseYear(List<MusicAlbum> albums);
    ReportResult GetByStudio(List<MusicAlbum> albums);
    ReportResult GetByDuration(List<MusicAlbum> albums);
}
