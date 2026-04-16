using System.Text;
using GallowsSpectre.Enums;
using GallowsSpectre.Helpers;
using GallowsSpectre.Interfaces;
using GallowsSpectre.Models;

namespace GallowsSpectre.Services;

public class EncryptedWordRepository : IWordRepository
{
    private readonly string _filePath;
    private readonly string _plainTextFilePath;
    private readonly IWordEncryptionService _encryptionService;
    private readonly Random _random = new();

    public EncryptedWordRepository(string filePath, IWordEncryptionService encryptionService)
    {
        _filePath = filePath;
        _plainTextFilePath = Path.Combine(Path.GetDirectoryName(filePath) ?? string.Empty, "words.txt");
        _encryptionService = encryptionService;
        EnsureFileExists();
        ExportPlainTextWordList();
    }

    public List<WordEntry> GetAll()
    {
        var result = new List<WordEntry>();

        foreach (string line in File.ReadAllLines(_filePath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string decrypted = _encryptionService.Decrypt(line.Trim());
            string[] parts = decrypted.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
            {
                continue;
            }

            if (!Enum.TryParse(parts[0], true, out DifficultyLevel difficulty))
            {
                continue;
            }

            result.Add(new WordEntry
            {
                Difficulty = difficulty,
                Value = parts[1].ToLowerInvariant()
            });
        }

        return result;
    }

    public string GetRandomWord(DifficultyLevel difficulty)
    {
        List<WordEntry> words = GetAll()
            .Where(word => word.Difficulty == difficulty)
            .ToList();

        if (words.Count == 0)
        {
            throw new InvalidOperationException($"No words found for difficulty '{difficulty}'.");
        }

        return words[_random.Next(words.Count)].Value;
    }

    private void EnsureFileExists()
    {
        string? directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (File.Exists(_filePath))
        {
            return;
        }

        List<string> encryptedLines = WordSeedData.GetDefaultWords()
            .Select(word => _encryptionService.Encrypt($"{word.Difficulty}|{word.Value}"))
            .ToList();

        File.WriteAllLines(_filePath, encryptedLines);
    }

    private void ExportPlainTextWordList()
    {
        List<WordEntry> words = GetAll()
            .OrderBy(word => word.Difficulty)
            .ThenBy(word => word.Value)
            .ToList();

        var sb = new StringBuilder();
        sb.AppendLine("Decrypted words from encrypted source");
        sb.AppendLine(new string('=', 36));
        sb.AppendLine();

        foreach (var group in words.GroupBy(word => word.Difficulty))
        {
            sb.AppendLine($"[{group.Key}]");
            foreach (WordEntry word in group)
            {
                sb.AppendLine(word.Value);
            }
            sb.AppendLine();
        }

        File.WriteAllText(_plainTextFilePath, sb.ToString());
    }
}
