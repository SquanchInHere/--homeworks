using Album.Interfaces;
using Album.Services;
using Album.Storages;

string baseDirectory = AppContext.BaseDirectory;
string dataDirectory = Path.Combine(baseDirectory, "Data");
string inputFilePath = Path.Combine(dataDirectory, "albums.json");

IAlbumStorage storage = new JsonAlbumStorage();
IAlbumService albumService = new AlbumService();
IReportService reportService = new ReportService();

var loadedAlbums = storage.Load(inputFilePath);
albumService.ReplaceAll(loadedAlbums);

var menuService = new ConsoleMenuService(albumService, storage, reportService);
menuService.Run();