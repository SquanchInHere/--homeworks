using TodoList.Src.Services;
using TaskStatus = TodoList.Src.Enums.TaskStatus;
using Task = TodoList.Src.Models.Task;

namespace TodoList.Src
{
    public static class Menu
    {
        public static void Run()
        {
            TaskManager taskManager = new TaskManager();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("=== TO-DO LIST MENU ===");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. Show all tasks");
                Console.WriteLine("3. Update task status");
                Console.WriteLine("4. Show tasks by status");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTaskMenu(taskManager);
                        break;

                    case "2":
                        taskManager.ShowAllTasks();
                        break;

                    case "3":
                        UpdateTaskStatusMenu(taskManager);
                        break;

                    case "4":
                        ShowTasksByStatusMenu(taskManager);
                        break;

                    case "0":
                        isRunning = false;
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void AddTaskMenu(TaskManager taskManager)
        {
            string title = ReadRequiredString("Enter task title: ");
            string description = ReadRequiredString("Enter task description: ");

            try
            {
                Task task = new Task(title, description);
                taskManager.AddTask(task);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void UpdateTaskStatusMenu(TaskManager taskManager)
        {
            taskManager.ShowAllTasks();

            int taskNumber = ReadInt("Enter task number to update: ");
            TaskStatus newStatus = ReadStatus();

            taskManager.UpdateTaskStatus(taskNumber, newStatus);
        }

        private static void ShowTasksByStatusMenu(TaskManager taskManager)
        {
            TaskStatus status = ReadStatus();
            taskManager.ShowTasksByStatus(status);
        }

        private static int ReadInt(string message)
        {
            int value;

            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                    return value;

                Console.WriteLine("Invalid number. Try again.");
            }
        }

        private static string ReadRequiredString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("Input cannot be empty. Try again.");
            }
        }

        private static TaskStatus ReadStatus()
        {
            while (true)
            {
                Console.WriteLine("Choose task status:");
                foreach (var status in Enum.GetValues(typeof(TaskStatus)))
                {
                    Console.WriteLine($"{(int)status} - {status}");
                }

                Console.Write("Enter status number: ");

                if (int.TryParse(Console.ReadLine(), out int statusNumber) &&
                    Enum.IsDefined(typeof(TaskStatus), statusNumber))
                {
                    return (TaskStatus)statusNumber;
                }

                Console.WriteLine("Invalid status. Try again.");
            }
        }
    }
}
