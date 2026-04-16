using System.Text.Json;
using GallowsSpectre.Interfaces;
using GallowsSpectre.Models;

namespace GallowsSpectre.Services;

public class WordStatisticsService : IWordStatisticsService
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public WordStatisticsService(string filePath)
    {
        _filePath = filePath;
        EnsureFileExists();
    }

    public void RegisterGame(string word, bool isWin)
    {
        List<WordStatistics> statistics = GetAll();

        WordStatistics item = statistics.FirstOrDefault(x =>
            x.Word.Equals(word, StringComparison.OrdinalIgnoreCase))
            ?? new WordStatistics { Word = word };

        if (!statistics.Any(x => x.Word.Equals(word, StringComparison.OrdinalIgnoreCase)))
        {
            statistics.Add(item);
        }

        item.GamesPlayed++;
        if (isWin)
        {
            item.Wins++;
        }
        else
        {
            item.Losses++;
        }

        SaveAll(statistics);
    }

    public List<WordStatistics> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return new List<WordStatistics>();
        }

        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<WordStatistics>>(json, _jsonOptions) ?? new List<WordStatistics>();
    }

    private void SaveAll(List<WordStatistics> statistics)
    {
        string json = JsonSerializer.Serialize(statistics, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }

    private void EnsureFileExists()
    {
        string? directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }
}
