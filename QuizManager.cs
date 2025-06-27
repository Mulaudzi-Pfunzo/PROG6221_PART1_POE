using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatBotGUIFinal
{
    public static class QuizManager
    {
        public class Question
        {
            public string Text { get; set; }
            public List<string> Options { get; set; }
            public int CorrectIndex { get; set; }
            public string Explanation { get; set; }
        }

        private static List<Question> questions = new List<Question>
        {
            new Question
            {
                Text = "What should you do if you receive an email asking for your password?",
                Options = new List<string> { "Reply with password", "Ignore it", "Report as phishing", "Click the link" },
                CorrectIndex = 2,
                Explanation = "Always report phishing emails instead of replying or clicking suspicious links."
            },
            new Question
            {
                Text = "What does 2FA stand for?",
                Options = new List<string> { "Two-Factor Authentication", "Two-Firewall Access", "Total File Access", "Tech For All" },
                CorrectIndex = 0,
                Explanation = "2FA means Two-Factor Authentication — a method to secure your accounts."
            },
            new Question
            {
                Text = "Which of the following is a strong password?",
                Options = new List<string> { "mypassword123", "password", "T!m3To$ecure", "abc123" },
                CorrectIndex = 2,
                Explanation = "Strong passwords include symbols, upper/lowercase letters, and numbers."
            },
            new Question
            {
                Text = "What is phishing?",
                Options = new List<string> { "Catching fish online", "A hacking technique", "Spam email", "Using fake websites to steal info" },
                CorrectIndex = 3,
                Explanation = "Phishing uses fake websites/emails to trick you into giving personal info."
            },
            new Question
            {
                Text = "Why should you update your software?",
                Options = new List<string> { "For new colors", "To get emojis", "To improve performance", "To patch security holes" },
                CorrectIndex = 3,
                Explanation = "Updates often fix critical security vulnerabilities."
            },
            new Question
            {
                Text = "What is the safest way to use public Wi-Fi?",
                Options = new List<string> { "Just browse freely", "Log in normally", "Use a VPN", "Send passwords via email" },
                CorrectIndex = 2,
                Explanation = "VPNs encrypt your connection on public Wi-Fi, keeping your data safe."
            },
            new Question
            {
                Text = "True or False: You should reuse your passwords for convenience.",
                Options = new List<string> { "True", "False", "Depends", "Not sure" },
                CorrectIndex = 1,
                Explanation = "Never reuse passwords — it makes all your accounts vulnerable if one gets hacked."
            },
            new Question
            {
                Text = "Which of the following is NOT a good cybersecurity practice?",
                Options = new List<string> { "Using 2FA", "Sharing your password with friends", "Regular backups", "Avoiding suspicious links" },
                CorrectIndex = 1,
                Explanation = "Never share your password — even with people you trust."
            },
            new Question
            {
                Text = "How often should you back up your data?",
                Options = new List<string> { "Once a year", "Only after a crash", "Weekly or regularly", "Never" },
                CorrectIndex = 2,
                Explanation = "Regular backups protect your data from loss due to malware or accidents."
            },
            new Question
            {
                Text = "What should you check before entering info on a website?",
                Options = new List<string> { "URL starts with https", "The font looks nice", "It's colorful", "There's an image" },
                CorrectIndex = 0,
                Explanation = "Secure sites use HTTPS to protect your data during transmission."
            }
        };

        // ✅ Return a question at the given index (or null if out of bounds)
        public static Question GetQuestion(int index)
        {
            if (index >= 0 && index < questions.Count)
            {
                return questions[index];
            }
            return null;
        }

        // ✅ Return total number of quiz questions
        public static int TotalQuestions()
        {
            return questions.Count;
        }
    }
}
