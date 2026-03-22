namespace CatalogLib.Src.Helpers
{
    public static class InputHelper
    {
        public static string ReadString(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }

        public static string ReadOptionalString(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }

        public static int ReadInt(string message)
        {
            Console.Write(message);
            return int.Parse(Console.ReadLine() ?? "0");
        }

        public static DateTime ReadDate(string message)
        {
            Console.Write(message);
            return DateTime.Parse(Console.ReadLine() ?? string.Empty);
        }

        public static List<string> ReadStringList(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine() ?? string.Empty;

            return new List<string>(
                input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            );
        }
    }
}
