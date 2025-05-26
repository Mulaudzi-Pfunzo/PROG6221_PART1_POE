using System;

namespace CyberChatBotApp
{
    static class ResponseHandlers
    {
        public delegate void ResponseDelegate();

        // ✅ Pointer to chatbot's TypingEffect method
        public static Action<string> TypingEffectCallback;

        public static void HandlePasswordHelp()
        {
            TypingEffectCallback?.Invoke("Use strong passwords with uppercase, numbers, and symbols.");
        }

        public static void HandleAccountSafety()
        {
            TypingEffectCallback?.Invoke("Review account activity regularly for suspicious login attempts.");
        }

        public static void HandleLockDevice()
        {
            TypingEffectCallback?.Invoke("Always lock your device when not in use — even at home.");
        }
    }
}
