using System;
using System.Collections.Generic;

namespace CyberBot_POE
{
    public class chatbot
    {
        public string UserName { get; set; } = "User";
        private bool isFirstMessage = true;
        private Random random = new Random();

        // Memory Variables (Requirement 4 & 5)
        private string lastTopic = "";
        private string favoriteTopic = "";
        private string lastBotQuestion = ""; // NEW: Tracks what the bot just asked you

        public string HandleUserQuery(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "I'm sorry, I didn't catch that. Could you try again?";
            string cleanInput = input.ToLower().Trim();

            // 1. Personalized Onboarding
            if (isFirstMessage)
            {
                UserName = input;
                isFirstMessage = false;
                return $"Hello {UserName}! I'm ShieldBot. I'm here to help you navigate the digital world safely. Is there a specific cybersecurity topic you're curious about today?";
            }

            // 2. Handling Answers to Small Talk (e.g., "How was your day?")
            if (lastBotQuestion == "how_was_day")
            {
                lastBotQuestion = "";
                return $"I'm glad to hear that, {UserName}! It's always good to check in. Now, back to business—was there anything about passwords or scams you wanted to ask me?";
            }

            // 3. Handling Yes/No to "Do you want another example?"
            if (lastBotQuestion == "want_more" || lastBotQuestion == "was_helpful")
            {
                lastBotQuestion = "";
                if (cleanInput.Contains("yes") || cleanInput.Contains("sure") || cleanInput.Contains("yep") || cleanInput.Contains("please"))
                {
                    return $"Happy to help, {UserName}! Here is another perspective on {lastTopic}: {GetTopicDetail(lastTopic)} \n\nShould I keep going, or is that enough for now?";
                }
                else if (cleanInput.Contains("no") || cleanInput.Contains("i'm good") || cleanInput.Contains("stop"))
                {
                    return $"No problem at all, {UserName}. I'll stay quiet until you need me again. What's next on your mind?";
                }
            }

            // 4. Handling "I have a question"
            if (cleanInput.Contains("have a question") || cleanInput.Contains("can i ask"))
            {
                return $"Of course, {UserName}! I'm listening. Please go ahead and ask your question.";
            }

            // 5. Handling "Tell me more" / "Another example" (Step 3 & 4)
            if (cleanInput.Contains("more") || cleanInput.Contains("another") || cleanInput.Contains("explain"))
            {
                if (string.IsNullOrEmpty(lastTopic))
                    return $"I'd love to explain more, {UserName}! But what should we talk about? (e.g. Passwords, Scams, Wi-Fi)";

                return $"Certainly, {UserName}. Here is another example regarding {lastTopic}: {GetTopicDetail(lastTopic)} \n\nWould you like one more, or does that make sense?";
            }

            // 6. Topic Detection (Step 2: Definitions)
            string detectedTopic = DetermineTopic(cleanInput);
            if (!string.IsNullOrEmpty(detectedTopic))
            {
                lastTopic = detectedTopic;
                lastBotQuestion = "was_helpful";
                return GetTopicDefinition(detectedTopic);
            }

            // 7. Small Talk
            if (cleanInput.Contains("how are you"))
            {
                lastBotQuestion = "how_was_day";
                return $"I'm doing great, {UserName}, thank you for asking! I'm feeling fully powered. How has your day been so far?";
            }

            // 8. Storing Interests (Step 5)
            if (cleanInput.Contains("interested in") || cleanInput.Contains("like learning"))
            {
                favoriteTopic = DetermineTopic(cleanInput);
                if (!string.IsNullOrEmpty(favoriteTopic))
                    return $"I've noted that {favoriteTopic} is a priority for you, {UserName}! Would you like the basic definition of that topic?";
            }

            // 9. Recall Advice
            if (cleanInput.Contains("advice") || cleanInput.Contains("tip"))
            {
                if (!string.IsNullOrEmpty(favoriteTopic))
                    return $"Since you're interested in {favoriteTopic}, {UserName}, here is a pro-tip: {GetTopicDetail(favoriteTopic)}";

                return $"I'd be happy to give some advice, {UserName}! Are you worried about passwords, social media, or maybe public Wi-Fi?";
            }

            // Final Fallback
            return $"I'm not 100% sure I follow, {UserName}. Could you try rephrasing that for me?";
        }

        private string DetermineTopic(string input)
        {
            if (input.Contains("password")) return "passwords";
            if (input.Contains("scam")) return "online scams";
            if (input.Contains("social") || input.Contains("media")) return "social media safety";
            if (input.Contains("wifi") || input.Contains("wi-fi")) return "public wi-fi security";
            if (input.Contains("2fa") || input.Contains("factor") || input.Contains("auth")) return "two-factor authentication";
            return "";
        }

        private string GetTopicDefinition(string topic)
        {
            string def = "";
            switch (topic)
            {
                case "passwords": def = "Passwords are secret strings used to verify your identity."; break;
                case "online scams": def = "Online scams are fraudulent schemes tricking you into giving away money or data."; break;
                case "social media safety": def = "This is about the settings you use to stay private on platforms like Instagram."; break;
                case "public wi-fi security": def = "This is the practice of protecting your data on open networks in public places."; break;
                case "two-factor authentication": def = "2FA is a security layer that requires two forms of ID to access an account."; break;
            }
            return $"Sure, {UserName}. {def}\n\nWould you like me to give you a real-world example of this?";
        }

        // Expanded Tips for Randomization (Step 3)
        private string GetTopicDetail(string topic)
        {
            List<string> tips = new List<string>();
            if (topic == "passwords")
            {
                tips.Add("Using a passphrase like 'Coffee-Durban-Sunset-2026' is much harder to crack than 'Password123'.");
                tips.Add("I always recommend using a Password Manager so you don't have to write them down.");
                tips.Add("Adding a special character in the middle of a word is better than putting it at the end!");
            }
            else if (topic == "online scams")
            {
                tips.Add("A common SA scam is a fake SMS saying your bank account is blocked—don't click the link!");
                tips.Add("Be careful of 'Crypto' experts on Instagram promising to double your money in a week.");
                tips.Add("If someone calls from 'Microsoft' saying your PC has a virus, just hang up.");
            }
            else
            {
                tips.Add("Always keep your phone's software updated to the latest version.");
                tips.Add("Think twice before clicking any link that creates a sense of urgency.");
            }

            return tips[random.Next(tips.Count)];
        }
    }
}