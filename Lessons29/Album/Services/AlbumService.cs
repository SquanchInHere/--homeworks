using Album.Interfaces;
using Album.Models;
using Album.Records;

namespace Album.Services;

public class AlbumService : IAlbumService
{
    private List<MusicAlbum> _albums = new();

    public List<MusicAlbum> GetAll() => _albums;

    public MusicAlbum AddAlbum(AlbumCreateRecord record)
    {
        var album = new MusicAlbum
        {
            Id = Guid.NewGuid(),
            AlbumTitle = record.AlbumTitle,
            ArtistName = record.ArtistName,
            ReleaseYear = record.ReleaseYear,
            StudioName = record.StudioName,
            Songs = record.Songs.Select(MapSong).ToList()
        };

        _albums.Add(album);
        return album;
    }

    public bool DeleteAlbum(Guid albumId)
    {
        var album = _albums.FirstOrDefault(a => a.Id == albumId);
        if (album == null)
        {
            return false;
        }

        _albums.Remove(album);
        return true;
    }

    public bool UpdateAlbum(Guid albumId, AlbumCreateRecord record)
    {
        var album = _albums.FirstOrDefault(a => a.Id == albumId);
        if (album == null)
        {
            return false;
        }

        album.AlbumTitle = record.AlbumTitle;
        album.ArtistName = record.ArtistName;
        album.ReleaseYear = record.ReleaseYear;
        album.StudioName = record.StudioName;
        album.Songs = record.Songs.Select(MapSong).ToList();
        return true;
    }

    public bool AddSongToAlbum(Guid albumId, SongCreateRecord songRecord)
    {
        var album = _albums.FirstOrDefault(a => a.Id == albumId);
        if (album == null)
        {
            return false;
        }

        album.Songs.Add(MapSong(songRecord));
        return true;
    }

    public bool DeleteSongFromAlbum(Guid albumId, Guid songId)
    {
        var album = _albums.FirstOrDefault(a => a.Id == albumId);
        if (album == null)
        {
            return false;
        }

        var song = album.Songs.FirstOrDefault(s => s.Id == songId);
        if (song == null)
        {
            return false;
        }

        album.Songs.Remove(song);
        return true;
    }

    public bool UpdateSong(Guid albumId, Guid songId, SongCreateRecord songRecord)
    {
        var song = GetSongById(albumId, songId);
        if (song == null)
        {
            return false;
        }

        song.Title = songRecord.Title;
        song.DurationSeconds = songRecord.DurationSeconds;
        song.LyricsFilePath = songRecord.LyricsFilePath;
        return true;
    }

    public List<MusicAlbum> SearchAlbums(AlbumSearchRecord record)
    {
        IEnumerable<MusicAlbum> query = _albums;

        if (!string.IsNullOrWhiteSpace(record.AlbumTitle))
        {
            query = query.Where(a => a.AlbumTitle.Contains(record.AlbumTitle, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(record.ArtistName))
        {
            query = query.Where(a => a.ArtistName.Contains(record.ArtistName, StringComparison.OrdinalIgnoreCase));
        }

        if (record.ReleaseYear.HasValue)
        {
            query = query.Where(a => a.ReleaseYear == record.ReleaseYear.Value);
        }

        if (!string.IsNullOrWhiteSpace(record.StudioName))
        {
            query = query.Where(a => a.StudioName.Contains(record.StudioName, StringComparison.OrdinalIgnoreCase));
        }

        if (record.MaxDurationSeconds.HasValue)
        {
            query = query.Where(a => a.TotalDurationSeconds <= record.MaxDurationSeconds.Value);
        }

        if (!string.IsNullOrWhiteSpace(record.SongTitle))
        {
            query = query.Where(a => a.Songs.Any(s => s.Title.Contains(record.SongTitle, StringComparison.OrdinalIgnoreCase)));
        }

        return query.ToList();
    }

    public MusicAlbum? GetAlbumById(Guid albumId)
    {
        return _albums.FirstOrDefault(a => a.Id == albumId);
    }

    public Song? GetSongById(Guid albumId, Guid songId)
    {
        return _albums
            .FirstOrDefault(a => a.Id == albumId)?
            .Songs
            .FirstOrDefault(s => s.Id == songId);
    }

    public string? ReadSongLyrics(Guid albumId, Guid songId)
    {
        var song = GetSongById(albumId, songId);
        if (song == null || string.IsNullOrWhiteSpace(song.LyricsFilePath))
        {
            return null;
        }

        if (!File.Exists(song.LyricsFilePath))
        {
            return null;
        }

        return File.ReadAllText(song.LyricsFilePath);
    }

    public void ReplaceAll(List<MusicAlbum> albums)
    {
        _albums = albums ?? new List<MusicAlbum>();
    }

    private static Song MapSong(SongCreateRecord record)
    {
        return new Song
        {
            Id = Guid.NewGuid(),
            Title = record.Title,
            DurationSeconds = record.DurationSeconds,
            LyricsFilePath = record.LyricsFilePath
        };
    }
}
