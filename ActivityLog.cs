using System;
using System.Collections.Generic;

namespace CyberChatBotGUIFinal
{
    public static class ActivityLog
    {
        private static readonly List<string> actions = new List<string>();

        /// <summary>
        /// Adds a new activity log entry with timestamp.
        /// </summary>
        public static void Add(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            actions.Add($"{timestamp} 📝 {message}");
        }

        /// <summary>
        /// Gets the 10 most recent actions (or fewer if less exist).
        /// </summary>
        public static List<string> GetRecentActions()
        {
            int count = actions.Count;
            int skip = Math.Max(0, count - 10);
            return actions.GetRange(skip, count - skip);
        }

        /// <summary>
        /// Clears the activity log entirely.
        /// </summary>
        public static void ClearLog()
        {
            actions.Clear();
        }
    }
}
