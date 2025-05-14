using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CyberChatBotApp
{
    static class CyberTips
    {
        // Dictionary mapping keywords to multiple tips
        private static readonly Dictionary<string, List<string>> tipsByKeyword = new Dictionary<string, List<string>>()
        {
            ["password"] = new List<string>
            {
                "Use complex passwords with numbers and symbols.",
                "Never reuse passwords across accounts.",
                "Consider using a password manager to store them safely."
            },
            ["phishing"] = new List<string>
            {
                "Don’t click suspicious links in emails.",
                "Verify the sender before responding to emails.",
                "Banks and companies will never ask for personal info via email."
            },
            ["safe browsing"] = new List<string>
            {
                "Use secure HTTPS websites.",
                "Keep your browser and antivirus updated.",
                "Avoid downloading unknown files or clicking popups."
            },
            ["2fa"] = new List<string>
            {
                "Two-Factor Authentication adds a second layer of security.",
                "Always enable 2FA on important accounts like email and banking.",
                "Use apps like Google Authenticator or SMS for 2FA codes."
            },
            ["public wifi"] = new List<string>
            {
                "Avoid entering passwords on public Wi-Fi.",
                "Use a VPN when browsing on public networks.",
                "Turn off sharing features when on public Wi-Fi."
            },
            ["updates"] = new List<string>
            {
                "Software updates fix security holes.",
                "Enable automatic updates when possible.",
                "Outdated software is a major security risk."
            },
            ["backup"] = new List<string>
            {
                "Back up your data regularly to avoid loss.",
                "Use cloud or external hard drives for backups.",
                "Ransomware can destroy local files — backups are your defense."
            }
        };

        // Returns a random response based on keyword
        public static string GetTip(string keyword)
        {
            if (tipsByKeyword.ContainsKey(keyword))
            {
                var responses = tipsByKeyword[keyword];
                var random = new Random();
                return responses[random.Next(responses.Count)];
            }
            return null;
        }

        // Used to check which keyword (if any) is present in input
        public static string FindKeyword(string userInput)
        {
            string lowerInput = userInput.ToLower();
            foreach (var keyword in tipsByKeyword.Keys)
            {
                if (lowerInput.Contains(keyword))
                    return keyword;
            }
            return null;
        }
    }
}
