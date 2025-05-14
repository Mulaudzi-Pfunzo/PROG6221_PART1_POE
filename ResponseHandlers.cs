using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatBotApp
{
    static class ResponseHandlers
    {
        // Define a delegate
        public delegate void ResponseDelegate();

        // Examples of actions tied to certain commands
        public static void HandlePasswordHelp()
        {
            Console.WriteLine("Need help creating strong passwords? Use a mix of letters, numbers, and symbols!");
        }

        public static void HandleAccountSafety()
        {
            Console.WriteLine("Remember to review account activity regularly for suspicious login attempts.");
        }

        public static void HandleLockDevice()
        {
            Console.WriteLine("Always lock your device when not in use — even at home.");
        }
    }
}
