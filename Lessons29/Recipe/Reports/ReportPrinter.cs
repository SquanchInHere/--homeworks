using Recipe.Models;

namespace Recipe.Reports;

public static class ReportPrinter
{
    public static void Print(ReportResult report)
    {
        Console.WriteLine();
        Console.WriteLine(report.Title);
        foreach (var line in report.Lines)
        {
            Console.WriteLine(line);
        }
    }

    public static void SaveToFile(string filePath, ReportResult report)
    {
        File.WriteAllText(filePath, report.ToString());
    }
}
