using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Media;


namespace CyberChatBotApp
{
    static class Utilities
    {
        public static void PlayVoiceGreeting()
        {
            try
            {
                string relativePath = @"Media\welcome.wav";
                string fullPath = Path.GetFullPath(relativePath);

                if (!File.Exists(fullPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error] Voice greeting file not found.");
                    return;
                }

                SoundPlayer player = new SoundPlayer(fullPath);
                player.Load();
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Warning] Voice greeting could not be played: " + ex.Message);
            }
        }

        public static void DisplayLogo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
  ██████╗ ███████╗██╗   █╗███╗   ██╗███████╗ ██████╗
  ██╔══██╗██╔════╝██║   ██║████╗  ██║╚══███╔╝██╔═══██╗
  ██████╔╝█████╗  ██║   ██║██╔██╗ ██║  ███╔╝ ██║   ██║
  ██╔═══╝ ██╔══╝  ██║   ██║██║╚██╗██║ ███╔╝  ██║   ██║
  ██║     ██║     ╚██████╔╝██║ ╚████║███████╗╚██████╔╝
  ╚═╝     ╚═╝      ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ 
    ");
        }
    }
}

