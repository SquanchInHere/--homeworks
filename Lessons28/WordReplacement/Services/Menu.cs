namespace WordReplacement.Services
{
    public static class Menu
    {
        public static void Run()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string dataDirectory = Path.Combine(baseDirectory, "Data");

            string inputFilePath = Path.Combine(dataDirectory, "Input.txt");
            string outputFilePath = Path.Combine(dataDirectory, "Output.txt");

            Console.WriteLine("=== Word Replacement ===");
            Console.WriteLine($"Input file:  {inputFilePath}");
            Console.WriteLine($"Output file: {outputFilePath}");
            Console.WriteLine();

            Console.Write("Enter the word to search: ");
            string searchWord = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the replacement word: ");
            string replacementWord = Console.ReadLine() ?? string.Empty;

            try
            {
                var service = new WordReplacementService();

                var result = service.ReplaceWords(
                    inputFilePath,
                    outputFilePath,
                    searchWord,
                    replacementWord);

                Console.WriteLine();
                Console.WriteLine("Operation completed successfully.");
                Console.WriteLine("Statistics:");
                Console.WriteLine($"Search word: {result.SearchWord}");
                Console.WriteLine($"Replacement word: {result.ReplacementWord}");
                Console.WriteLine($"Matches found: {result.MatchesFound}");
                Console.WriteLine($"Replacements made: {result.ReplacementsMade}");
                Console.WriteLine($"Original file size: {result.OriginalFileSizeBytes} bytes");
                Console.WriteLine($"Updated file size: {result.UpdatedFileSizeBytes} bytes");
                Console.WriteLine($"Execution time: {result.ExecutionTimeMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
