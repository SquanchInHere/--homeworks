using Album.Models;

namespace Album.Reports;

public static class ReportPrinter
{
    public static void Print(ReportResult report)
    {
        Console.WriteLine();
        Console.WriteLine(report.Title);
        foreach (string line in report.Lines)
        {
            Console.WriteLine(line);
        }
    }

    public static void SaveToFile(string filePath, ReportResult report)
    {
        File.WriteAllText(filePath, report.ToString());
    }
}
