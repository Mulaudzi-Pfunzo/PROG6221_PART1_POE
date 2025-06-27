using System.Windows;

namespace CyberChatBotGUIFinal
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var startup = new StartupWindow();
            startup.Show();
        }
    }
}


