namespace BaseLinq.Services;

public class DateTimeService
{
    public event Action<string>? OperationPerformed;

    public void ShowCurrentTime()
    {
        Console.WriteLine($"Current time: {DateTime.Now:HH:mm:ss}");
        OnOperationPerformed("Displayed current time");
    }

    public void ShowCurrentDate()
    {
        Console.WriteLine($"Current date: {DateTime.Now:dd.MM.yyyy}");
        OnOperationPerformed("Displayed current date");
    }

    public void ShowCurrentDayOfWeek()
    {
        Console.WriteLine($"Current day of week: {DateTime.Now.DayOfWeek}");
        OnOperationPerformed("Displayed current day of week");
    }

    private void OnOperationPerformed(string message)
    {
        OperationPerformed?.Invoke(message);
    }
}