using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatBotApp
{
    class CyberChatBot : Chatbot
    {
        private Dictionary<string, string> userMemory = new Dictionary<string, string>();
        private Dictionary<string, ResponseHandlers.ResponseDelegate> quickResponses;
        private Random random = new Random();

        // Cybersecurity topics and tips
        private Dictionary<string, List<string>> cybersecurityTips = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> {
                "Use strong passwords with uppercase, numbers, and symbols.",
                "Avoid personal info in passwords — like your name or birthdate.",
                "Use a password manager to safely store your passwords."
            }},
            { "phishing", new List<string> {
                "Phishing emails often pretend to be from trusted companies.",
                "Avoid clicking suspicious links or opening unknown attachments.",
                "Verify email addresses before responding to requests for info."
            }},
            { "safe browsing", new List<string> {
                "Always check that websites use HTTPS.",
                "Avoid downloading files from untrusted sites.",
                "Use updated antivirus software for protection while browsing."
            }},
            { "2fa", new List<string> {
                "Enable Two-Factor Authentication on all important accounts.",
                "Authenticator apps are safer than SMS for 2FA codes.",
                "2FA protects you even if your password is stolen."
            }},
            { "wifi", new List<string> {
                "Don’t log in to sensitive accounts over public Wi-Fi.",
                "Use a VPN when accessing personal data on open networks.",
                "Turn off automatic Wi-Fi connections when you don’t need them."
            }},
            { "updates", new List<string> {
                "Updates fix vulnerabilities hackers might exploit.",
                "Enable auto-updates for your OS and apps.",
                "Outdated software is a common security risk."
            }},
            { "backup", new List<string> {
                "Back up data weekly to a cloud or external drive.",
                "Ransomware can encrypt files — backups keep them safe.",
                "Automate backups so you don’t forget."
            }},
            { "privacy", new List<string> {
                "Limit what personal info you share online.",
                "Review privacy settings on apps and social media.",
                "Think before posting anything sensitive."
            }}
        };

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

            // Initialize quick-response delegates
            quickResponses = new Dictionary<string, ResponseHandlers.ResponseDelegate>
            {
                { "account safety", ResponseHandlers.HandleAccountSafety },
                { "lock device", ResponseHandlers.HandleLockDevice },
                { "password help", ResponseHandlers.HandlePasswordHelp }
            };
        }

        public override void Run()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("[Info] You can ask me about:");
            Console.WriteLine("- Passwords, phishing, safe browsing, 2FA");
            Console.WriteLine("- Public Wi-Fi, software updates, backups, privacy");
            Console.WriteLine("- Or just say how you feel (e.g. 'I'm worried about scams')");
            Console.WriteLine("- Try quick commands: password help, lock device, account safety");
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
            // Sentiment detection
            if (input.Contains("worried"))
                TypingEffect("It's okay to feel worried. Let's get you the info you need to feel safer.");
            else if (input.Contains("frustrated"))
                TypingEffect("Frustration is normal. I'm here to simplify cybersecurity for you.");
            else if (input.Contains("curious"))
                TypingEffect("I like curiosity! Let’s explore your questions.");
            else if (input.Contains("angry"))
                TypingEffect("Let’s calm things down. I'm here to support you, not stress you out.");
            else if (input.Contains("scared"))
                TypingEffect("No need to be scared. Taking small steps can go a long way to protect yourself.");

            // Memory feature
            if (input.Contains("i'm interested in") || input.Contains("i am interested in"))
            {
                string topic = input.Split("in").Last().Trim();
                if (!string.IsNullOrEmpty(topic))
                {
                    userMemory["interest"] = topic;
                    TypingEffect($"Got it! I'll remember that you're interested in {topic}.");
                    return;
                }
            }

            if (userMemory.ContainsKey("interest") && input.Contains("remind me"))
            {
                TypingEffect($"You previously mentioned you're interested in {userMemory["interest"]}. Here's a tip:");
                GiveCyberTip(userMemory["interest"]);
                return;
            }

            // Keyword-based tip system
            foreach (var keyword in cybersecurityTips.Keys)
            {
                if (input.Contains(keyword))
                {
                    GiveCyberTip(keyword);
                    return;
                }
            }

            // Delegate-based quick responses
            foreach (var entry in quickResponses)
            {
                if (input.Contains(entry.Key))
                {
                    entry.Value.Invoke();
                    return;
                }
            }

            // General fallback response
            if (input.Contains("how are you"))
                TypingEffect("I'm fully operational and ready to help!");
            else if (input.Contains("purpose"))
                TypingEffect("I'm designed to help you stay safe in the digital world.");
            else if (input.Contains("what can i ask") || input.Contains("help"))
                TypingEffect("Ask me about passwords, phishing, privacy, backups, updates, and more.");
            else
                TypingEffect("Hmm... I didn’t quite get that. Try asking about something related to cybersecurity.");
        }

        private void GiveCyberTip(string keyword)
        {
            if (cybersecurityTips.ContainsKey(keyword))
            {
                var tips = cybersecurityTips[keyword];
                string selectedTip = tips[random.Next(tips.Count)];
                TypingEffect(selectedTip);
            }
            else
            {
                TypingEffect("Sorry, I don’t have tips on that yet.");
            }
        }
    }
}