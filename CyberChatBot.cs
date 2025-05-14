using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatBotApp
{
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
            Console.WriteLine("- Tell me about 2FA / public Wi-Fi / software updates / data backups");
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
                TypingEffect("You can ask me about passwords, phishing, safe browsing, 2FA, public Wi-Fi risks, software updates, and data backups.");
            else if (input.Contains("password"))
                TypingEffect("Always use strong, unique passwords and enable two-factor authentication.");
            else if (input.Contains("phishing"))
                TypingEffect("Phishing is when attackers try to trick you into revealing personal info. Always double-check links and email senders.");
            else if (input.Contains("safe browsing"))
                TypingEffect("Avoid clicking on unknown links and use updated browsers and antivirus software.");
            else if (input.Contains("2fa") || input.Contains("two factor"))
                TypingEffect("Two-Factor Authentication (2FA) adds an extra layer of protection by requiring a code sent to your phone or app.");
            else if (input.Contains("wifi") || input.Contains("public wi-fi"))
                TypingEffect("Avoid entering sensitive information when using public Wi-Fi. Use a VPN for safer connections.");
            else if (input.Contains("update") || input.Contains("updates"))
                TypingEffect("Keep your software and operating system updated to patch security vulnerabilities.");
            else if (input.Contains("backup") || input.Contains("data backup"))
                TypingEffect("Regularly back up important data to an external drive or cloud to avoid loss from cyberattacks.");
            else
                TypingEffect("I didn’t quite get that. Could you rephrase or ask something else?");
        }
    }
}