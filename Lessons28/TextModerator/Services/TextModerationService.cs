using System.Text.RegularExpressions;
using TextModerator.Models;

namespace Task2.TextModerator.Services;

public class TextModerationService
{
    public ModerationResult ModerateText(
        string inputFilePath,
        string forbiddenWordsFilePath,
        string outputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException("Input text file was not found.", inputFilePath);
        }

        if (!File.Exists(forbiddenWordsFilePath))
        {
            throw new FileNotFoundException("Forbidden words file was not found.", forbiddenWordsFilePath);
        }

        string content = File.ReadAllText(inputFilePath);

        List<string> forbiddenWords = File.ReadAllLines(forbiddenWordsFilePath)
            .Select(line => line.Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        int totalReplacements = 0;

        foreach (string word in forbiddenWords)
        {
            string pattern = $@"\b{Regex.Escape(word)}\b";
            MatchCollection matches = Regex.Matches(content, pattern, RegexOptions.IgnoreCase);

            if (matches.Count > 0)
            {
                string replacement = new string('*', word.Length);
                content = Regex.Replace(content, pattern, replacement, RegexOptions.IgnoreCase);
                totalReplacements += matches.Count;
            }
        }

        string? outputDirectory = Path.GetDirectoryName(outputFilePath);
        if (!string.IsNullOrWhiteSpace(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        File.WriteAllText(outputFilePath, content);

        return new ModerationResult
        {
            InputFilePath = inputFilePath,
            ForbiddenWordsFilePath = forbiddenWordsFilePath,
            OutputFilePath = outputFilePath,
            ForbiddenWordsCount = forbiddenWords.Count,
            TotalReplacements = totalReplacements,
            ModeratedText = content
        };
    }
}