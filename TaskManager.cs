using System;
using System.Collections.Generic;

namespace CyberChatBotGUIFinal
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool Completed { get; set; }

        public override string ToString()
        {
            string status = Completed ? "✅ Completed" : "🕒 Pending";
            string reminder = ReminderDate.HasValue ? $" • Reminder: {ReminderDate.Value.ToShortDateString()}" : "";
            return $"{Title} - {Description}{reminder} [{status}]";
        }
    }

    public static class TaskManager
    {
        private static List<TaskItem> tasks = new();

        public static void AddTask(string title, string description, DateTime? reminderDate = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                return;

            if (TaskExists(title))
                return;

            tasks.Add(new TaskItem
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate,
                Completed = false
            });

            string logMsg = $"📝 Task added: \"{title}\"";
            if (reminderDate.HasValue)
                logMsg += $" (Reminder: {reminderDate.Value.ToShortDateString()})";

            ActivityLog.Add(logMsg);
        }

        public static List<TaskItem> GetAllTasks()
        {
            return new List<TaskItem>(tasks); // Optionally: sort here by date
        }

        public static void MarkAsCompleted(string title)
        {
            var task = tasks.Find(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (task != null)
            {
                task.Completed = true;
                ActivityLog.Add($"✅ Task completed: \"{title}\"");
            }
        }

        public static void DeleteTask(string title)
        {
            var task = tasks.Find(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (task != null)
            {
                tasks.Remove(task);
                ActivityLog.Add($"🗑️ Task deleted: \"{title}\"");
            }
        }

        public static List<TaskItem> GetDueReminders(DateTime date)
        {
            return tasks.FindAll(t =>
                t.ReminderDate.HasValue &&
                t.ReminderDate.Value.Date == date.Date &&
                !t.Completed);
        }

        public static bool TaskExists(string title)
        {
            return tasks.Exists(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }
    }
}
