using GallowsSpectre.Interfaces;
using GallowsSpectre.Renderers;
using GallowsSpectre.Services;

string dataDirectory = Path.Combine(AppContext.BaseDirectory, "Data");
string wordsFilePath = Path.Combine(dataDirectory, "words.dat");
string decryptedWordsFilePath = Path.Combine(dataDirectory, "words.txt");
string statisticsFilePath = Path.Combine(dataDirectory, "word-stats.json");

IWordEncryptionService encryptionService = new WordEncryptionService();
IWordRepository wordRepository = new EncryptedWordRepository(wordsFilePath, encryptionService);
IWordStatisticsService wordStatisticsService = new WordStatisticsService(statisticsFilePath);
IGameRenderer gameRenderer = new SpectreGameRenderer();
IGameService gameService = new GameService(wordRepository, wordStatisticsService, gameRenderer, decryptedWordsFilePath);

gameService.Run();
