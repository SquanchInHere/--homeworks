namespace BaseLinq.Handlers;

public class EventLogger
{
    public void OnOperationPerformed(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"[Event] {message}");
        Console.ResetColor();
    }
}
