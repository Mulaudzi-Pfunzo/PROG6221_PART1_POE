using System;
using System.IO;
using System.Media;
using System.Threading;
using CyberChatBotApp; 

namespace CyberChatBotApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Voice greeting and logo
            Utilities.PlayVoiceGreeting();
            Utilities.DisplayLogo();

            // Chat interaction
            Chatbot bot = new CyberChatBot();
            bot.GreetUser();
            bot.Run();

            Console.ResetColor();
            Console.ReadKey();
        }
    }
}