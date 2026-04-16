using Album.Models;

namespace Album.Interfaces;

public interface IAlbumStorage
{
    void Save(string filePath, List<MusicAlbum> albums);
    List<MusicAlbum> Load(string filePath);
}
