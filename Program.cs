using System;

namespace CyberBot
{
    /// <summary>
    /// The entry point of the application. 
    /// Manages the primary execution loop and onboarding.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Initialize the Chatbot and UI
            Chatbot myBot = new Chatbot();
            UserInterface.DisplayHeader();

            // Note: Ensure 'welcome2.wav' is in your /bin/Debug folder or comment this out for now
            UserInterface.PlayGreetingAudio("welcome2.wav");

            // 2. Personalized Onboarding with Validation
            UserInterface.TypeLine("Welcome to the Cyber-Awareness Portal. I am ShieldBot.");
            UserInterface.TypeLine("To provide you with a personalized experience, may I ask what your name is?");

            string name;
            do
            {
                Console.Write("\n[System] Please Enter Your Name: ");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    UserInterface.TypeLine("I need a name to address you properly. Please try again.", ConsoleColor.Red);
                }
            } while (string.IsNullOrWhiteSpace(name));

            myBot.UserName = name;

            UserInterface.TypeLine($"\nIt is a pleasure to meet you, {myBot.UserName}!", ConsoleColor.Cyan);
            UserInterface.TypeLine("I am ready to help you navigate the digital world safely. What is on your mind?");
            Console.WriteLine("----------------------------------------------------------------");

            // 3. Main Chat Loop
            bool isRunning = true;
            while (isRunning)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\n[{myBot.UserName}]: ");
                string input = Console.ReadLine();
                Console.ResetColor();

                // Check for exit commands
                if (!string.IsNullOrEmpty(input) &&
                   (input.ToLower() == "exit" || input.ToLower() == "quit"))
                {
                    isRunning = false;
                    UserInterface.TypeLine($"\nThank you for prioritizing your safety today, {myBot.UserName}. Goodbye!", ConsoleColor.Green);
                }
                else
                {
                    // Pass the logic handling to the Chatbot class
                    myBot.HandleUserQuery(input);
                }
            }
        }
    }
}