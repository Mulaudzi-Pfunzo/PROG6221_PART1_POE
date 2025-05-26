# CyberChatBot

A text-based console chatbot that promotes **cybersecurity awareness** using fundamental object-oriented programming (OOP) principles and GitHub CI (Continuous Integration). Created as part of the PROG6221 Part 1 Practical Assignment.

---

## Features

- Voice Greeting: Plays a recorded welcome message on startup
- ASCII Logo: Displays a stylized app logo in the console
- User Interaction: Greets the user by name and responds to various questions
- Cyber Safety Tips: Educates users about passwords, phishing, and safe browsing
- Exception Handling: To prevent crashes in cases of errors or incorrect input
- OOP Design: Uses abstract classes and inheritance for structure
- GitHub Actions: Runs automated CI build checks on every push

---

## Sample Questions You Can Ask

- How are you?
- What is your purpose?
- What can I ask you about?
- Tell me about passwords / phishing / safe browsing

---

## How to Run

1. Open in Visual Studio
2. Run the program
3. Wait for the greeting to finish
4. Interact with the bot using suggested questions
5. Type `exit` to end the chat

---

## Part 2 Enhancements

- **Quick Commands via Delegates**  
  Type "password help", "lock device", or "account safety" to trigger built-in quick responses.

- **Sentiment Responses**  
  The chatbot can recognize and respond to emotional input like:
  - I'm worried
  - I'm frustrated
  - I'm scared
  - I'm curious
  - I'm angry

- **Interest Memory Feature**  
  You can say:
  - I'm interested in updates  
  Then later say:
  - Remind me about updates  
  The bot will recall your interest and give a related tip.

- **Randomized Tip System (5+ per topic)**  
  Each topic contains **at least 5 random cyber tips**. You'll get a different response each time.

- **Modular Class Structure**  
  The chatbot is now split across multiple C# files:
  - `Chatbot.cs` (abstract class)
  - `CyberChatBot.cs` (inherited logic)
  - `CyberTips.cs` (cybersecurity tips)
  - `ResponseHandlers.cs` (delegate-based quick actions)
  - `Program.cs` (entry point)

---

## Topics You Can Ask About

- Passwords  
- Phishing  
- Safe browsing  
- 2FA (Two-Factor Authentication)  
- Public Wi-Fi  
- Software updates  
- Data backups  
- Online privacy

---

## GitHub Actions

GitHub Actions workflow automatically runs build checks after each commit to ensure continuous integration and working code.



GitHub Repository Link
https://github.com/Mulaudzi-Pfunzo/PROG6221_PART1_POE.git





