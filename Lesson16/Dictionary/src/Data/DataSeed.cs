namespace Dictionary.src.Data
{
    public static class DataSeed
    {
        public static Dictionary<string, List<string>> GetInitialData()
        {
            return new Dictionary<string, List<string>>
            {
                { "cat", new List<string> { "кіт", "кішка" } },
                { "dog", new List<string> { "пес" } },
                { "house", new List<string> { "будинок" } }
            };
        }
    }
}
