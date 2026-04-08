using System;
using System.Collections.Generic;
using System.Text;
using Task2.TextModerator.Services;

namespace TextModerator.Services
{
    public static class Menu
    {
        public static void Run()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string dataDirectory = Path.Combine(baseDirectory, "Data");

            string inputFilePath = Path.Combine(dataDirectory, "Input.txt");
            string forbiddenWordsFilePath = Path.Combine(dataDirectory, "Forbidden_words.txt");
            string outputFilePath = Path.Combine(dataDirectory, "Output.txt");

            Console.WriteLine("=== Task 2: Text Moderator ===");
            Console.WriteLine($"Input file:           {inputFilePath}");
            Console.WriteLine($"Forbidden words file: {forbiddenWordsFilePath}");
            Console.WriteLine($"Output file:          {outputFilePath}");
            Console.WriteLine();

            try
            {
                var service = new TextModerationService();

                var result = service.ModerateText(
                    inputFilePath,
                    forbiddenWordsFilePath,
                    outputFilePath);

                Console.WriteLine("Moderation completed successfully.");
                Console.WriteLine();
                Console.WriteLine("Moderated text:");
                Console.WriteLine(result.ModeratedText);
                Console.WriteLine();
                Console.WriteLine("Statistics:");
                Console.WriteLine($"Forbidden words count: {result.ForbiddenWordsCount}");
                Console.WriteLine($"Total replacements: {result.TotalReplacements}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
