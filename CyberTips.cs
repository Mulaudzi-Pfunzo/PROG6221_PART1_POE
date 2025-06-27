using System;
using System.Collections.Generic;

namespace CyberChatBotGUIFinal
{
    public static class CyberTips
    {
        private static readonly Dictionary<string, List<string>> tipsByKeyword = new()
        {
            ["password"] = new()
            {
                "Use complex passwords with numbers and symbols.",
                "Never reuse passwords across different accounts.",
                "Consider using a password manager to store them safely.",
                "Avoid using common passwords like '123456' or 'password'.",
                "Change your passwords regularly, especially after a breach."
            },
            ["phishing"] = new()
            {
                "Don’t click suspicious links in emails or messages.",
                "Verify the sender before replying to unexpected emails.",
                "Never enter personal info on suspicious websites.",
                "Hover over links to see where they really go before clicking.",
                "Watch for urgent language asking you to 'act now'."
            },
            ["safe browsing"] = new()
            {
                "Use websites that begin with HTTPS for secure connections.",
                "Keep your browser and antivirus software up to date.",
                "Avoid clicking on pop-ups or download links from shady sites.",
                "Never save passwords in a shared or public computer.",
                "Install an ad blocker to prevent malicious pop-up ads."
            },
            ["2fa"] = new()
            {
                "Two-Factor Authentication adds a second layer of security.",
                "Always enable 2FA on important accounts like email and banking.",
                "Use an authenticator app instead of SMS for stronger protection.",
                "Even if someone gets your password, 2FA helps stop them.",
                "Backup your 2FA codes in case you lose your device."
            },
            ["public wifi"] = new()
            {
                "Avoid entering passwords or personal info on public Wi-Fi.",
                "Use a VPN when browsing or accessing sensitive data.",
                "Turn off sharing features while using public networks.",
                "Always log out of accounts when done on public Wi-Fi.",
                "Public networks are not encrypted — treat them as unsafe."
            },
            ["updates"] = new()
            {
                "Software updates fix security vulnerabilities hackers exploit.",
                "Enable automatic updates for your OS, browser, and apps.",
                "Check for updates regularly if auto-update is disabled.",
                "Outdated software is a major target for cyberattacks.",
                "Install patches even if they seem minor — they protect you."
            },
            ["backup"] = new()
            {
                "Back up important data weekly to avoid loss.",
                "Use cloud storage or external drives for backups.",
                "Backups protect you from ransomware and system failure.",
                "Test your backups to make sure they can be restored.",
                "Keep at least one backup offline or disconnected from the internet."
            }
        };

        public static string GetTip(string keyword)
        {
            if (tipsByKeyword.ContainsKey(keyword))
            {
                var responses = tipsByKeyword[keyword];
                var random = new Random();
                return responses[random.Next(responses.Count)];
            }
            return null!;
        }

        public static string FindKeyword(string userInput)
        {
            string lowerInput = userInput.ToLower();
            foreach (var keyword in tipsByKeyword.Keys)
            {
                if (lowerInput.Contains(keyword))
                    return keyword;
            }
            return null!;
        }
        public static List<string> GetAvailableTipTopics()
        {
            return new List<string>(tipsByKeyword.Keys);
        }
    }
}
