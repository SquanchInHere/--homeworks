using System.Diagnostics;
using System.Text.RegularExpressions;
using WordReplacement.Models;

namespace WordReplacement.Services;

public class WordReplacementService
{
    public ReplacementStatistics ReplaceWords(
        string inputFilePath,
        string outputFilePath,
        string searchWord,
        string replacementWord)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException("Input file was not found.", inputFilePath);
        }

        if (string.IsNullOrWhiteSpace(searchWord))
        {
            throw new ArgumentException("Search word cannot be empty.", nameof(searchWord));
        }

        var stopwatch = Stopwatch.StartNew();

        string content = File.ReadAllText(inputFilePath);
        long originalSize = new FileInfo(inputFilePath).Length;

        string pattern = $@"\b{Regex.Escape(searchWord)}\b";
        MatchCollection matches = Regex.Matches(content, pattern, RegexOptions.IgnoreCase);

        string updatedContent = Regex.Replace(content, pattern, replacementWord, RegexOptions.IgnoreCase);

        string? outputDirectory = Path.GetDirectoryName(outputFilePath);
        if (!string.IsNullOrWhiteSpace(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        File.WriteAllText(outputFilePath, updatedContent);

        stopwatch.Stop();

        long updatedSize = new FileInfo(outputFilePath).Length;

        return new ReplacementStatistics
        {
            InputFilePath = inputFilePath,
            OutputFilePath = outputFilePath,
            SearchWord = searchWord,
            ReplacementWord = replacementWord,
            MatchesFound = matches.Count,
            ReplacementsMade = matches.Count,
            OriginalFileSizeBytes = originalSize,
            UpdatedFileSizeBytes = updatedSize,
            ExecutionTimeMilliseconds = stopwatch.ElapsedMilliseconds
        };
    }
}