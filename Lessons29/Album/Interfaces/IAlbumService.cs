using Album.Models;
using Album.Records;

namespace Album.Interfaces;

public interface IAlbumService
{
    List<MusicAlbum> GetAll();
    MusicAlbum AddAlbum(AlbumCreateRecord record);
    bool DeleteAlbum(Guid albumId);
    bool UpdateAlbum(Guid albumId, AlbumCreateRecord record);
    bool AddSongToAlbum(Guid albumId, SongCreateRecord songRecord);
    bool DeleteSongFromAlbum(Guid albumId, Guid songId);
    bool UpdateSong(Guid albumId, Guid songId, SongCreateRecord songRecord);
    List<MusicAlbum> SearchAlbums(AlbumSearchRecord record);
    MusicAlbum? GetAlbumById(Guid albumId);
    Song? GetSongById(Guid albumId, Guid songId);
    string? ReadSongLyrics(Guid albumId, Guid songId);
    void ReplaceAll(List<MusicAlbum> albums);
}
