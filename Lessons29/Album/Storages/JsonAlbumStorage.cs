using System.Text.Json;
using Album.Interfaces;
using Album.Models;

namespace Album.Storages;

public class JsonAlbumStorage : IAlbumStorage
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public void Save(string filePath, List<MusicAlbum> albums)
    {
        string json = JsonSerializer.Serialize(albums, _options);
        File.WriteAllText(filePath, json);
    }

    public List<MusicAlbum> Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<MusicAlbum>();
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<MusicAlbum>>(json, _options) ?? new List<MusicAlbum>();
    }
}
