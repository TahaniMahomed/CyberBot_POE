using System;

namespace CyberBot
{
    public class Chatbot
    {
        public string BotName { get; set; } = "ShieldBot";
        public string UserName { get; set; }

        /// <summary>
        /// Processes the user's string input and provides detailed, localized safety advice.
        /// </summary>
        public void HandleUserQuery(string input)
        {
            // 1. Validation: Handle empty or accidental whitespace
            if (string.IsNullOrWhiteSpace(input))
            {
                UserInterface.TypeLine($"I'm sorry, {UserName}, I didn't catch that. Could you please type your question again?", ConsoleColor.Red);
                return;
            }

            string cleanInput = input.ToLower().Trim();

            // 2. Response Logic: Detailed and Engaging
            if (cleanInput.Contains("purpose") || cleanInput.Contains("who are you"))
            {
                UserInterface.TypeLine($"That's a great question, {UserName}! My primary mission is to educate South African citizens on digital safety.");
                UserInterface.TypeLine("I'm here to help you recognize local cyber-crimes and protect your personal information in our digital economy.");
            }
            else if (cleanInput.Contains("phishing") || cleanInput.Contains("email"))
            {
                UserInterface.TypeLine($"Listen closely, {UserName}, phishing is a major threat in SA right now.", ConsoleColor.Yellow);
                UserInterface.TypeLine("Scammers often pretend to be banks like FNB, ABSA, or Standard Bank. They might send an SMS or Email saying your account is blocked.");
                UserInterface.TypeLine("Tip: Never click those links! A real bank will never ask for your PIN or password via a link.");
            }
            else if (cleanInput.Contains("password"))
            {
                UserInterface.TypeLine($"Security starts with a strong foundation, {UserName}!", ConsoleColor.Green);
                UserInterface.TypeLine("Avoid using simple things like '12345' or your birthday. Instead, use a 'passphrase'—four random words joined together.");
                UserInterface.TypeLine("Example: 'Blue-Table-Mountain-Sun!' is much harder for a hacker to crack than a single word.");
            }
            else if (cleanInput.Contains("scam") || cleanInput.Contains("money"))
            {
                UserInterface.TypeLine($"{UserName}, if an online 'investment' or 'prize' sounds too good to be true, it almost certainly is.", ConsoleColor.Yellow);
                UserInterface.TypeLine("Be very careful with WhatsApp messages offering quick cash or jobs that ask for an upfront 'registration fee'.");
            }
            else if (cleanInput.Contains("virus") || cleanInput.Contains("malware"))
            {
                UserInterface.TypeLine($"I understand your concern about viruses, {UserName}.", ConsoleColor.Red);
                UserInterface.TypeLine("A virus can steal your files or spy on your typing. To stay safe, ensure your Windows Defender is active and never download 'cracked' software.");
            }
            else if (cleanInput.Contains("how are you"))
            {
                UserInterface.TypeLine($"I'm functioning perfectly and feeling ready to protect, {UserName}! I'm glad you asked.");
                UserInterface.TypeLine("How are you finding the portal so far? Is there a specific security topic you're worried about?");
            }
            else if (cleanInput.Contains("help") || cleanInput.Contains("menu"))
            {
                UserInterface.TypeLine($"Of course, {UserName}! I can help you with several topics.");
                UserInterface.TypeLine("You can ask me about: \n- 'Phishing' (Fake bank messages)\n- 'Passwords' (How to stay secure)\n- 'Scams' (Online fraud)\n- 'Viruses' (Protecting your PC)");
            }
            else if (cleanInput.Contains("identity") || cleanInput.Contains("id"))
            {
                UserInterface.TypeLine($"{UserName}, identity theft is serious. Never share your SA ID number or a photo of your ID on social media.");
                UserInterface.TypeLine("If you lose your phone, contact your bank immediately to secure your apps.");
            }
            // 3. Fallback: Friendly guidance when keywords aren't met
            else
            {
                UserInterface.TypeLine($"I'm not quite sure about that specific topic yet, {UserName}, but I am constantly learning!", ConsoleColor.Gray);
                UserInterface.TypeLine("Try asking me about 'Phishing', 'Passwords', 'Scams', or 'Identity Theft'. I'm here to help!");
            }
        }
    }
}