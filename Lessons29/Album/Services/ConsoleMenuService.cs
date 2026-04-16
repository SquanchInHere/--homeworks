using Album.Helpers;
using Album.Interfaces;
using Album.Models;
using Album.Reports;
using Album.Records;

namespace Album.Services;

public class ConsoleMenuService
{
    private readonly IAlbumService _albumService;
    private readonly IAlbumStorage _storage;
    private readonly IReportService _reportService;

    private const string StorageFilePath = "Data/albums.json";
    private const string ReportsDirectory = "Data/reports";

    public ConsoleMenuService(IAlbumService albumService, IAlbumStorage storage, IReportService reportService)
    {
        _albumService = albumService;
        _storage = storage;
        _reportService = reportService;
    }

    public void Run()
    {
        while (true)
        {
            PrintMainMenu();
            string input = ConsoleHelper.ReadString("Choose an option: ");

            switch (input)
            {
                case "1": AddAlbum(); break;
                case "2": DeleteAlbum(); break;
                case "3": UpdateAlbum(); break;
                case "4": AddSongToAlbum(); break;
                case "5": DeleteSongFromAlbum(); break;
                case "6": UpdateSongInAlbum(); break;
                case "7": SearchAlbums(); break;
                case "8": ShowAllAlbums(); break;
                case "9": SaveAlbums(); break;
                case "10": LoadAlbums(); break;
                case "11": ShowSongLyrics(); break;
                case "12": GenerateReport(); break;
                case "0": Console.WriteLine("Goodbye."); return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private static void PrintMainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== MUSIC ALBUM APP =====");
        Console.WriteLine("1. Add album");
        Console.WriteLine("2. Delete album");
        Console.WriteLine("3. Update album song list and album data");
        Console.WriteLine("4. Add song to album");
        Console.WriteLine("5. Delete song from album");
        Console.WriteLine("6. Update song in album");
        Console.WriteLine("7. Search albums");
        Console.WriteLine("8. Show all albums");
        Console.WriteLine("9. Save albums to file");
        Console.WriteLine("10. Load albums from file");
        Console.WriteLine("11. Show song lyrics");
        Console.WriteLine("12. Generate report");
        Console.WriteLine("0. Exit");
    }

    private void AddAlbum()
    {
        AlbumCreateRecord record = ReadAlbumCreateRecord();
        MusicAlbum album = _albumService.AddAlbum(record);
        Console.WriteLine("Album added successfully.");
        Console.WriteLine(album);
    }

    private void DeleteAlbum()
    {
        ShowAlbumsShort();
        string idText = ConsoleHelper.ReadString("Enter album ID to delete: ");
        if (!Guid.TryParse(idText, out Guid albumId))
        {
            Console.WriteLine("Invalid album ID.");
            return;
        }

        bool result = _albumService.DeleteAlbum(albumId);
        Console.WriteLine(result ? "Album deleted." : "Album not found.");
    }

    private void UpdateAlbum()
    {
        ShowAlbumsShort();
        string idText = ConsoleHelper.ReadString("Enter album ID to update: ");
        if (!Guid.TryParse(idText, out Guid albumId))
        {
            Console.WriteLine("Invalid album ID.");
            return;
        }

        var existingAlbum = _albumService.GetAlbumById(albumId);
        if (existingAlbum == null)
        {
            Console.WriteLine("Album not found.");
            return;
        }

        AlbumCreateRecord record = ReadAlbumCreateRecord();
        bool result = _albumService.UpdateAlbum(albumId, record);
        Console.WriteLine(result ? "Album updated." : "Album not found.");
    }

    private void AddSongToAlbum()
    {
        ShowAlbumsShort();
        string idText = ConsoleHelper.ReadString("Enter album ID: ");
        if (!Guid.TryParse(idText, out Guid albumId))
        {
            Console.WriteLine("Invalid album ID.");
            return;
        }

        SongCreateRecord record = ReadSongCreateRecord();
        bool result = _albumService.AddSongToAlbum(albumId, record);
        Console.WriteLine(result ? "Song added to album." : "Album not found.");
    }

    private void DeleteSongFromAlbum()
    {
        ShowAlbumsWithSongsShort();
        string albumIdText = ConsoleHelper.ReadString("Enter album ID: ");
        string songIdText = ConsoleHelper.ReadString("Enter song ID to delete: ");

        if (!Guid.TryParse(albumIdText, out Guid albumId) || !Guid.TryParse(songIdText, out Guid songId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        bool result = _albumService.DeleteSongFromAlbum(albumId, songId);
        Console.WriteLine(result ? "Song deleted." : "Album or song not found.");
    }

    private void UpdateSongInAlbum()
    {
        ShowAlbumsWithSongsShort();
        string albumIdText = ConsoleHelper.ReadString("Enter album ID: ");
        string songIdText = ConsoleHelper.ReadString("Enter song ID to update: ");

        if (!Guid.TryParse(albumIdText, out Guid albumId) || !Guid.TryParse(songIdText, out Guid songId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        SongCreateRecord record = ReadSongCreateRecord();
        bool result = _albumService.UpdateSong(albumId, songId, record);
        Console.WriteLine(result ? "Song updated." : "Album or song not found.");
    }

    private void SearchAlbums()
    {
        Console.WriteLine();
        Console.WriteLine("===== SEARCH MENU =====");
        Console.WriteLine("1. By album title");
        Console.WriteLine("2. By artist");
        Console.WriteLine("3. By release year");
        Console.WriteLine("4. By studio");
        Console.WriteLine("5. By maximum duration");
        Console.WriteLine("6. By song title");

        string input = ConsoleHelper.ReadString("Choose search type: ");

        AlbumSearchRecord searchRecord = input switch
        {
            "1" => new AlbumSearchRecord(AlbumTitle: ConsoleHelper.ReadString("Enter album title: ")),
            "2" => new AlbumSearchRecord(ArtistName: ConsoleHelper.ReadString("Enter artist: ")),
            "3" => new AlbumSearchRecord(ReleaseYear: ConsoleHelper.ReadInt("Enter release year: ")),
            "4" => new AlbumSearchRecord(StudioName: ConsoleHelper.ReadString("Enter studio: ")),
            "5" => new AlbumSearchRecord(MaxDurationSeconds: ConsoleHelper.ReadInt("Enter maximum duration in seconds: ")),
            "6" => new AlbumSearchRecord(SongTitle: ConsoleHelper.ReadString("Enter song title: ")),
            _ => new AlbumSearchRecord()
        };

        List<MusicAlbum> results = _albumService.SearchAlbums(searchRecord);
        if (results.Count == 0)
        {
            Console.WriteLine("No albums found.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== SEARCH RESULTS =====");
        foreach (var album in results)
        {
            Console.WriteLine(album);
            Console.WriteLine(new string('-', 60));
        }
    }

    private void ShowAllAlbums()
    {
        List<MusicAlbum> albums = _albumService.GetAll();
        if (albums.Count == 0)
        {
            Console.WriteLine("No albums available.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== ALL ALBUMS =====");
        foreach (var album in albums)
        {
            Console.WriteLine(album);
            Console.WriteLine(new string('-', 60));
        }
    }

    private void SaveAlbums()
    {
        try
        {
            _storage.Save(StorageFilePath, _albumService.GetAll());
            Console.WriteLine($"Albums saved to '{StorageFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Save error: {ex.Message}");
        }
    }

    private void LoadAlbums()
    {
        try
        {
            List<MusicAlbum> albums = _storage.Load(StorageFilePath);
            _albumService.ReplaceAll(albums);
            Console.WriteLine($"Albums loaded from '{StorageFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Load error: {ex.Message}");
        }
    }

    private void ShowSongLyrics()
    {
        ShowAlbumsWithSongsShort();
        string albumIdText = ConsoleHelper.ReadString("Enter album ID: ");
        string songIdText = ConsoleHelper.ReadString("Enter song ID: ");

        if (!Guid.TryParse(albumIdText, out Guid albumId) || !Guid.TryParse(songIdText, out Guid songId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        string? lyrics = _albumService.ReadSongLyrics(albumId, songId);
        if (string.IsNullOrWhiteSpace(lyrics))
        {
            Console.WriteLine("Lyrics file is missing or empty.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== SONG LYRICS =====");
        Console.WriteLine(lyrics);
    }

    private void GenerateReport()
    {
        Console.WriteLine();
        Console.WriteLine("===== REPORT MENU =====");
        Console.WriteLine("1. Report by artist");
        Console.WriteLine("2. Report by release year");
        Console.WriteLine("3. Report by studio");
        Console.WriteLine("4. Report by duration");

        string input = ConsoleHelper.ReadString("Choose report type: ");
        List<MusicAlbum> albums = _albumService.GetAll();

        var report = input switch
        {
            "1" => _reportService.GetByArtist(albums),
            "2" => _reportService.GetByReleaseYear(albums),
            "3" => _reportService.GetByStudio(albums),
            "4" => _reportService.GetByDuration(albums),
            _ => null
        };

        if (report == null)
        {
            Console.WriteLine("Invalid option.");
            return;
        }

        ReportPrinter.Print(report);

        string saveChoice = ConsoleHelper.ReadString("Save report to file? (y/n): ");
        if (saveChoice.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            string fileName = ConsoleHelper.ReadString("Enter report file name (without extension): ");
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = "report";
            }

            string path = Path.Combine(ReportsDirectory, $"{fileName}.txt");
            ReportPrinter.SaveToFile(path, report);
            Console.WriteLine($"Report saved to '{path}'.");
        }
    }

    private void ShowAlbumsShort()
    {
        var albums = _albumService.GetAll();
        if (albums.Count == 0)
        {
            Console.WriteLine("No albums available.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== ALBUMS =====");
        foreach (var album in albums)
        {
            Console.WriteLine($"{album.Id} | {album.AlbumTitle} | {album.ArtistName} | {album.ReleaseYear} | {album.TotalDurationSeconds} sec");
        }
    }

    private void ShowAlbumsWithSongsShort()
    {
        var albums = _albumService.GetAll();
        if (albums.Count == 0)
        {
            Console.WriteLine("No albums available.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== ALBUMS AND SONGS =====");
        foreach (var album in albums)
        {
            Console.WriteLine($"Album: {album.Id} | {album.AlbumTitle} | {album.ArtistName}");
            foreach (var song in album.Songs)
            {
                Console.WriteLine($"   Song: {song.Id} | {song.Title} | {song.DurationSeconds} sec");
            }
        }
    }

    private static AlbumCreateRecord ReadAlbumCreateRecord()
    {
        string albumTitle = ConsoleHelper.ReadString("Album title: ");
        string artistName = ConsoleHelper.ReadString("Artist name: ");
        int releaseYear = ConsoleHelper.ReadInt("Release year: ");
        string studioName = ConsoleHelper.ReadString("Studio name: ");
        int songCount = ConsoleHelper.ReadInt("Number of songs: ");

        var songs = new List<SongCreateRecord>();
        for (int i = 0; i < songCount; i++)
        {
            Console.WriteLine($"Song #{i + 1}");
            songs.Add(ReadSongCreateRecord());
        }

        return new AlbumCreateRecord(albumTitle, artistName, releaseYear, studioName, songs);
    }

    private static SongCreateRecord ReadSongCreateRecord()
    {
        string title = ConsoleHelper.ReadString("Song title: ");
        int durationSeconds = ConsoleHelper.ReadInt("Song duration in seconds: ");
        string lyricsPath = ConsoleHelper.ReadString("Lyrics file path (leave empty if none): ");
        return new SongCreateRecord(title, durationSeconds, string.IsNullOrWhiteSpace(lyricsPath) ? null : lyricsPath);
    }
}
