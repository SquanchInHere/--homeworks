using TaskStatus = TodoList.Src.Enums.TaskStatus;


namespace TodoList.Src.Models
{
    public class Task
    {
        private string title;
        private string description;

        public string Title => title;
        public string Description => description;
        public TaskStatus Status { get; set; }

        public Task(string title, string description, TaskStatus status = TaskStatus.NotStarted)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Task title cannot be empty.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Task description cannot be empty.");

            this.title = title.Trim();
            this.description = description.Trim();
            Status = status;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Description: {Description}, Status: {Status}";
        }
    }
}
