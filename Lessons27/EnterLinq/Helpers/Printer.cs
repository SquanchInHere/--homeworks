namespace EnterLinq.Helpers;

public static class Printer
{
    public static void PrintTitle(string title)
    {
        Console.WriteLine($"=== {title} ===");
    }

    public static void PrintCollection<T>(string title, IEnumerable<T> items)
    {
        PrintTitle(title);

        foreach (T item in items)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }

    public static void PrintSingle<T>(string title, T? item)
    {
        PrintTitle(title);

        if (item == null)
        {
            Console.WriteLine("No data.");
        }
        else
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }
}
