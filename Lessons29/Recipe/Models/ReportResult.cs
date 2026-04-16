namespace Recipe.Models;

public class ReportResult
{
    public string Title { get; set; } = string.Empty;
    public List<string> Lines { get; set; } = new();

    public override string ToString()
    {
        return $"{Title}{Environment.NewLine}{string.Join(Environment.NewLine, Lines)}";
    }
}
