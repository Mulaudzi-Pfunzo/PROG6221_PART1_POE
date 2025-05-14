using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatBotApp
{
    abstract class Chatbot
    {
        public string UserName { get; set; }

        public abstract void GreetUser();
        public abstract void Run();
        protected abstract void RespondToInput(string input);

        protected void TypingEffect(string message, int delay = 30)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delay);
            }
            Console.WriteLine("\n");
        }
    }
}
