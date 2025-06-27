using System.Windows;

namespace CyberChatBotGUIFinal
{
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
        }

        // Triggered when the "Enter App →" button is clicked
        private void EnterApp_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();

            // Validate user input
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(
                    "Please enter your name to continue.",
                    "Name Required",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            // Store user name for session use
            UserSession.UserName = name;

            // Launch the main application window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Close the startup screen
            this.Close();
        }
    }
}

