using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using CyberChatBotApp;

namespace CyberChatBotGUIFinal
{
    public partial class MainWindow : Window
    {
        private int currentQuizIndex = 0;
        private int quizScore = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WelcomeTextBlock.Text = $"👋 Hi {UserSession.UserName}, welcome to CyberChatBot!";
            RefreshTaskList();
            LoadQuizQuestion();
            RefreshActivityLog();

            PrintBotMessage(
                $"Hello {UserSession.UserName}! I'm CyberBot.\n" +
                "Here’s what I can help you with:\n" +
                "• Add, delete, or complete tasks\n" +
                "• Set reminders\n" +
                "• Show activity logs\n" +
                "• Ask for cyber safety tips (e.g. 'Tell me about phishing')\n" +
                "• Type 'What can you do?' to see all features"
            );
        }

        // ========================= CHAT =========================

        private void UserInputButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                PrintUserMessage("[empty]");
                PrintBotMessage("Please enter a message.");
                return;
            }

            PrintUserMessage(input);

            string lowerInput = input.ToLower();

            if (lowerInput.StartsWith("add task") || lowerInput.Contains("create task"))
            {
                string title = ExtractAfterKeyword(lowerInput, "task");
                if (!string.IsNullOrWhiteSpace(title))
                {
                    TaskManager.AddTask(title, "Task added via chatbot");
                    PrintBotMessage($"✅ Task \"{title}\" added.");
                    RefreshTaskList(); // 🔥 Fix for showing in Task Assistant
                }
                else
                {
                    PrintBotMessage("Please provide a task title.");
                }
            }
            else if (lowerInput.StartsWith("remind me to") || lowerInput.Contains("set reminder"))
            {
                string reminder = ExtractAfterKeyword(lowerInput, "remind me to");
                if (!string.IsNullOrWhiteSpace(reminder))
                {
                    TaskManager.AddTask(reminder, "Reminder from chatbot", DateTime.Today.AddDays(1));
                    PrintBotMessage($"⏰ Reminder set for tomorrow to '{reminder}'.");
                    RefreshTaskList(); // 🔥 Fix for showing in Task Assistant
                }
                else
                {
                    PrintBotMessage("I couldn’t detect what to remind you about.");
                }
            }
            else if (lowerInput.Contains("activity log") || lowerInput.Contains("show log"))
            {
                var log = ActivityLog.GetRecentActions();
                if (log.Count == 0)
                {
                    PrintBotMessage("No recent actions yet.");
                }
                else
                {
                    PrintBotMessage("📋 Recent actions:\n" + string.Join("\n• ", log));
                }
            }
            else if (lowerInput.StartsWith("mark task") && lowerInput.Contains("complete"))
            {
                string title = ExtractBetween(lowerInput, "mark task", "complete");
                if (!string.IsNullOrWhiteSpace(title))
                {
                    TaskManager.MarkAsCompleted(title);
                    PrintBotMessage($"✅ Marked task '{title}' as completed.");
                    RefreshTaskList();
                }
                else
                {
                    PrintBotMessage("Please specify the task to mark as completed.");
                }
            }
            else if (lowerInput.StartsWith("delete task") || lowerInput.Contains("remove task"))
            {
                string title = ExtractAfterKeyword(lowerInput, "delete task");
                if (!string.IsNullOrWhiteSpace(title))
                {
                    TaskManager.DeleteTask(title);
                    PrintBotMessage($"🗑️ Task '{title}' deleted.");
                    RefreshTaskList();
                }
                else
                {
                    PrintBotMessage("Please specify the task to delete.");
                }
            }
            else if (lowerInput.Contains("what can you do") || lowerInput.Contains("features") || lowerInput.Contains("tips"))
            {
                var tips = CyberTips.GetAvailableTipTopics();
                PrintBotMessage("🔧 I can help with the following topics:\n" +
                    "• Add, delete, or complete tasks\n" +
                    "• Set reminders\n" +
                    "• Show activity logs\n" +
                    "• Take a cybersecurity quiz\n" +
                    "• Give safety tips like:\n" + string.Join("\n- ", tips));
            }
            else
            {
                string keyword = CyberTips.FindKeyword(lowerInput);
                if (keyword != null)
                {
                    string tip = CyberTips.GetTip(keyword);
                    PrintBotMessage($"🔐 {tip}");
                }
                else
                {
                    PrintBotMessage("🤔 I didn't understand. Try asking about tasks, reminders, or cyber tips.");
                }
            }

            UserInputBox.Clear();
            RefreshActivityLog();
        }

        private void PrintUserMessage(string message)
        {
            Paragraph p = new Paragraph
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            p.Inlines.Add(new Run($"{UserSession.UserName}: {message}")
            {
                Foreground = Brushes.LightSkyBlue,
                FontWeight = FontWeights.SemiBold
            });
            NlpOutputRichTextBox.Document.Blocks.Add(p);
            NlpOutputRichTextBox.ScrollToEnd();
        }

        private void PrintBotMessage(string message)
        {
            Paragraph p = new Paragraph
            {
                Margin = new Thickness(0, 0, 0, 15)
            };
            p.Inlines.Add(new Run($"Bot: {message}")
            {
                Foreground = Brushes.LightGreen,
                FontWeight = FontWeights.Normal
            });
            NlpOutputRichTextBox.Document.Blocks.Add(p);
            NlpOutputRichTextBox.ScrollToEnd();
        }

        private string ExtractAfterKeyword(string input, string keyword)
        {
            int index = input.IndexOf(keyword);
            return index >= 0 ? input[(index + keyword.Length)..].Trim(' ', '.', ':', '-', '"') : "";
        }

        private string ExtractBetween(string input, string start, string end)
        {
            int startIndex = input.IndexOf(start);
            int endIndex = input.IndexOf(end);
            return (startIndex >= 0 && endIndex > startIndex)
                ? input.Substring(startIndex + start.Length, endIndex - startIndex - start.Length).Trim()
                : "";
        }

        // ========================= TASKS =========================

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            string description = TaskDescBox.Text.Trim();
            DateTime? reminder = ReminderDatePicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            {
                StatusText.Text = "⚠️ Please fill in both title and description.";
                return;
            }

            TaskManager.AddTask(title, description, reminder);
            StatusText.Text = $"✅ Task '{title}' added.";
            TaskTitleBox.Clear();
            TaskDescBox.Clear();
            ReminderDatePicker.SelectedDate = null;
            RefreshTaskList();
            RefreshActivityLog();
        }

        private void MarkTaskCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskItem selected)
            {
                TaskManager.MarkAsCompleted(selected.Title);
                StatusText.Text = $"✅ Marked '{selected.Title}' as completed.";
                RefreshTaskList();
                RefreshActivityLog();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskItem selected)
            {
                TaskManager.DeleteTask(selected.Title);
                StatusText.Text = $"🗑️ Deleted task '{selected.Title}'.";
                RefreshTaskList();
                RefreshActivityLog();
            }
        }

        private void RefreshTaskList()
        {
            TaskListBox.ItemsSource = null;
            TaskListBox.ItemsSource = TaskManager.GetAllTasks();
        }

        private void RefreshActivityLog()
        {
            ActivityLogListBox.ItemsSource = null;
            ActivityLogListBox.ItemsSource = ActivityLog.GetRecentActions();
        }

        // ========================= QUIZ =========================

        private void LoadQuizQuestion()
        {
            var question = QuizManager.GetQuestion(currentQuizIndex);
            if (question == null)
            {
                QuizQuestionText.Text = $"🎉 {UserSession.UserName}, the quiz is complete!\nFinal score: {quizScore}/{QuizManager.TotalQuestions()}";
                OptionAButton.Visibility = OptionBButton.Visibility = OptionCButton.Visibility = OptionDButton.Visibility = Visibility.Collapsed;
                NextQuestionButton.Visibility = Visibility.Collapsed;
                return;
            }

            QuizQuestionText.Text = $"🧠 Question {currentQuizIndex + 1} of {QuizManager.TotalQuestions()}:\n{question.Text}";
            OptionAButton.Content = $"A) {question.Options[0]}";
            OptionBButton.Content = $"B) {question.Options[1]}";
            OptionCButton.Content = $"C) {question.Options[2]}";
            OptionDButton.Content = $"D) {question.Options[3]}";
            QuizFeedbackText.Text = "";
            EnableQuizButtons(true);
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;
            int selectedIndex = clicked == OptionAButton ? 0 :
                                clicked == OptionBButton ? 1 :
                                clicked == OptionCButton ? 2 : 3;

            var question = QuizManager.GetQuestion(currentQuizIndex);
            if (selectedIndex == question.CorrectIndex)
            {
                quizScore++;
                QuizFeedbackText.Text = $"✅ Correct, {UserSession.UserName}!\n{question.Explanation}";
            }
            else
            {
                QuizFeedbackText.Text = $"❌ Incorrect, {UserSession.UserName}.\n{question.Explanation}";
            }

            ActivityLog.Add($"Quiz Q{currentQuizIndex + 1}: {(selectedIndex == question.CorrectIndex ? "Correct" : "Incorrect")}");
            RefreshActivityLog();
            EnableQuizButtons(false);
        }

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            currentQuizIndex++;
            LoadQuizQuestion();
        }

        private void EnableQuizButtons(bool enable)
        {
            OptionAButton.IsEnabled = OptionBButton.IsEnabled = OptionCButton.IsEnabled = OptionDButton.IsEnabled = enable;
            NextQuestionButton.IsEnabled = !enable;
        }

        // ========================= VIEW SWITCHING =========================

        private void ShowTaskView(object sender, RoutedEventArgs e)
        {
            TaskView.Visibility = Visibility.Visible;
            QuizView.Visibility = ChatView.Visibility = LogView.Visibility = Visibility.Collapsed;
        }

        private void ShowQuizView(object sender, RoutedEventArgs e)
        {
            QuizView.Visibility = Visibility.Visible;
            TaskView.Visibility = ChatView.Visibility = LogView.Visibility = Visibility.Collapsed;
        }

        private void ShowChatView(object sender, RoutedEventArgs e)
        {
            ChatView.Visibility = Visibility.Visible;
            TaskView.Visibility = QuizView.Visibility = LogView.Visibility = Visibility.Collapsed;
        }

        private void ShowLogView(object sender, RoutedEventArgs e)
        {
            LogView.Visibility = Visibility.Visible;
            TaskView.Visibility = QuizView.Visibility = ChatView.Visibility = Visibility.Collapsed;
        }
    }
}
