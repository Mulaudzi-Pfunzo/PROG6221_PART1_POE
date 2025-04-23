using System;
using System.IO;
using System.Media;
using System.Threading;

namespace CyberChatBotApp
{
    // Abstract base class: Defines structure for any chatbot
    abstract class Chatbot
    {
        public string UserName { get; set; }

        public abstract void GreetUser();
        public abstract void Run();
        protected abstract void RespondToInput(string input);

        // Optional shared helper method for all bots
        protected void TypingEffect(string message, int delay = 30)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine("\n");
        }
    }









    // Concrete class that inherits from Chatbot
    class CyberChatBot : Chatbot
    {
        public override void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n>> Please enter your name: ");
            UserName = Console.ReadLine()?.Trim();

            while (string.IsNullOrWhiteSpace(UserName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Error] Name cannot be empty. Please try again.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(">> Enter your name: ");
                UserName = Console.ReadLine()?.Trim();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[Welcome] Hello, {UserName}! I’m CyberChatBot — your guide to staying safe online.\n");
        }

        public override void Run()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("[Info] You can ask me things like:");
            Console.WriteLine("- How are you?");
            Console.WriteLine("- What is your purpose?");
            Console.WriteLine("- What can I ask you about?");
            Console.WriteLine("- Tell me about passwords / phishing / safe browsing");
            Console.WriteLine("[Type 'exit' to quit]\n");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("You: ");
                string input = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error] Please enter something for me to respond to.");
                    continue;
                }

                if (input == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n[Goodbye] Take care, {UserName}! Stay safe online.");
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                RespondToInput(input);
            }
        }

        protected override void RespondToInput(string input)
        {
            if (input.Contains("how are you"))
                TypingEffect("I'm fully operational and always ready to help!");
            else if (input.Contains("purpose"))
                TypingEffect("My mission is to help you stay cyber-safe by sharing tips and answering questions.");
            else if (input.Contains("what can i ask") || input.Contains("help"))
                TypingEffect("You can ask me about passwords, phishing, or safe browsing habits.");
            else if (input.Contains("password"))
                TypingEffect("Always use strong, unique passwords and enable two-factor authentication.");
            else if (input.Contains("phishing"))
                TypingEffect("Phishing is when attackers try to trick you into revealing personal info. Always double-check links and email senders.");
            else if (input.Contains("safe browsing"))
                TypingEffect("Avoid clicking on unknown links and use updated browsers and antivirus software.");
            else
                TypingEffect("I didn’t quite get that. Could you rephrase or ask something else?");
        }
    }






    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Play welcome voice greeting
            PlayVoiceGreeting();

            // Step 2: Display ASCII logo
            DisplayLogo();

            // Step 3: Create chatbot instance and interact
            Chatbot bot = new CyberChatBot();
            bot.GreetUser();
            bot.Run();

            Console.ResetColor();

            Console.ReadKey();
        }

        // Voice greeting with some exception handling
        static void PlayVoiceGreeting()
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
                player.PlaySync(); // Wait for greeting to finish
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Warning] Voice greeting could not be played: " + ex.Message);
            }
        }

        static void DisplayLogo()
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

