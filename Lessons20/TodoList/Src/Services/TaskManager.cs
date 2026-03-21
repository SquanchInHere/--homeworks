using TaskStatus = TodoList.Src.Enums.TaskStatus;
using Task = TodoList.Src.Models.Task;

namespace TodoList.Src.Services
{
    public class TaskManager
    {
        private readonly List<Task> tasks = new List<Task>();

        public void AddTask(Task task)
        {
            tasks.Add(task);
            Console.WriteLine("Task added successfully.");
        }

        public void ShowAllTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            Console.WriteLine("\nAll tasks:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]}");
            }
        }

        public void ShowTasksByStatus(TaskStatus status)
        {
            bool found = false;

            Console.WriteLine($"\nTasks with status {status}:");

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Status == status)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No tasks found with this status.");
            }
        }

        public void UpdateTaskStatus(int taskNumber, TaskStatus newStatus)
        {
            if (taskNumber < 1 || taskNumber > tasks.Count)
            {
                Console.WriteLine("Invalid task number.");
                return;
            }

            tasks[taskNumber - 1].Status = newStatus;
            Console.WriteLine("Task status updated successfully.");
        }
    }
}
