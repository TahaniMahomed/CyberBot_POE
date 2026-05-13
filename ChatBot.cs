/* S!--CODE ATTRIBUTION
TITLE: Microsoft Learn: Dictionaries in C#
AUTHOR: Microsoft .NET Team
DATE: 13 May 2026
VERSION: .NET 8.0
AVAILABLE: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/dictionary
*/

/* S!--CODE ATTRIBUTION
TITLE: Video: C# Dictionary Explained
AUTHOR: FreeCodeCamp (YouTube Channel)
DATE: 13 May 2026
VERSION: Video Tutorial
AVAILABLE: https://www.youtube.com/watch?v=ytCz0xkGd3g
*/

/* S!--CODE ATTRIBUTION
TITLE: Microsoft Learn: LINQ .Any Method
AUTHOR: Microsoft .NET Team
DATE: 13 May 2026
VERSION: LINQ Documentation
AVAILABLE: https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.any
*/

/* S!--CODE ATTRIBUTION
TITLE: Article: String Manipulation in C#
AUTHOR: Microsoft .NET Team
DATE: 13 May 2026
VERSION: C# String Methods
AVAILABLE: https://learn.microsoft.com/en-us/dotnet/csharp/how-to/modify-string-contents
*/

/* S!--CODE ATTRIBUTION
TITLE: Blog: Building a Simple Chatbot Logic in C#
AUTHOR: Independent Developer Blog
DATE: 13 May 2026
VERSION: Article Publication
AVAILABLE: https://www.c-sharpcorner.com/article/how-to-build-a-simple-chatbot-in-c-sharp/
*/

/* S!--CODE ATTRIBUTION
TITLE: Video: Logic and Decision Making in C#
AUTHOR: Programming Tutorials (YouTube Channel)
DATE: 13 May 2026
VERSION: Video Tutorial
AVAILABLE: https://www.youtube.com/watch?v=QwGx7uXfFfM
*/

/* S!--CODE ATTRIBUTION
TITLE: Microsoft Learn: Random Class
AUTHOR: Microsoft .NET Team
DATE: 13 May 2026
VERSION: .NET 8.0
AVAILABLE: https://learn.microsoft.com/en-us/dotnet/api/system.random
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBot_POE
{
    public class ChatBot
    {
        // --- Identity and Randomization Setup ---
        public string UserName { get; set; } = "User";
        private bool isFirstMessage = true;
        private readonly Random random = new Random();

        // --- Memory and Tracking Logic ---
        // lastTopic keeps track of the current discussion for flow control
        private string lastTopic = "";

        // Part 2 Requirement: FavoriteTopic handles "Memory and Recall"
        public string FavoriteTopic { get; set; } = "";

        // usedTips ensures we don't repeat advice in the same topic session
        private List<string> usedTips = new List<string>();

        // completedTopics tracks what the user has already learned to help filter the "No" response
        private List<string> completedTopics = new List<string>();

        // --- Keyword Mapping ---
        // Links user words to internal topic keys. Updated to include requested keywords.
        private readonly Dictionary<string, string> keywordMap = new Dictionary<string, string>
        {
            { "password", "passwords" },
            { "scam", "online scams" },
            { "phish", "phishing" },
            { "email", "phishing" },
            { "privacy", "data privacy" },
            { "safe browsing", "safe browsing" },
            { "browsing", "safe browsing" }
        };

        // --- South African Content Definitions ---
        // Definitions tailored to the local context (POPIA, SA Banks) as requested
        private readonly Dictionary<string, string> topicDefinitions = new Dictionary<string, string>
        {
            { "passwords", "Passwords are your first line of defense. In South Africa, banking fraud often starts with a stolen password." },
            { "online scams", "Online scams in SA are common, especially on platforms like Facebook Marketplace or via fake courier SMSes." },
            { "phishing", "Phishing is when scammers pretend to be your bank (like FNB, Standard Bank, or Capitec) to steal your login details." },
            { "data privacy", "Data privacy in South Africa is protected by the POPI Act (POPIA), which gives you rights over your personal info." },
            { "safe browsing", "Safe browsing means avoiding 'dodgy' websites that might install malware or track your location." }
        };

        private readonly List<string> negativeKeywords = new List<string> { "worried", "scared", "stressed", "confused", "frustrated" };
        private readonly List<string> continuationKeywords = new List<string> { "more", "tip", "example", "yes", "another", "tell me more" };

        // --- MAIN INPUT HANDLER ---
        public string HandleUserQuery(string input)
        {
            // Validating that the user actually provided input
            if (string.IsNullOrWhiteSpace(input))
                return "I noticed you didn't type anything! I'm here whenever you're ready.";

            string cleanInput = input.ToLower().Trim();

            // First Interaction: Sets the User Name for personalization
            if (isFirstMessage)
            {
                UserName = input;
                isFirstMessage = false;
                return $"Hello {UserName}! I'm Aegis-X, your South African Cybersecurity Sentinel. What would you like to ask me about today?";
            }

            // General Response Block: Handles basic identity and purpose questions
            if (cleanInput.Contains("how are you"))
                return $"I am functioning at 100% efficiency, {UserName}! How are you doing today?";

            if (cleanInput.Contains("purpose") || cleanInput.Contains("who are you"))
                return "I am Aegis-X. My purpose is to help South Africans stay safe from digital fraudsters and hackers.";

            if (cleanInput.Contains("what can i ask") || cleanInput.Contains("help"))
                return "You can ask me about: Passwords, Phishing, Scams, Privacy, or Safe Browsing!";

            // --- "NO" FACTOR LOGIC ---
            // Handles when a user declines more tips. It shows them what they HAVEN'T asked yet.
            if (cleanInput == "no" || cleanInput.Contains("no thank you") || cleanInput.Contains("stop"))
            {
                // Filter the topic keys to find what hasn't been added to completedTopics yet
                var remainingTopics = topicDefinitions.Keys.Where(t => !completedTopics.Contains(t)).ToList();

                if (remainingTopics.Count > 0)
                {
                    // Formats the list nicely (Uppercase first letter) to show the user their options
                    string topicList = string.Join(", ", remainingTopics.Select(t => t.First().ToString().ToUpper() + t.Substring(1)));
                    return $"No problem, {UserName}! What other topics are you interested in? I can still tell you about: {topicList}.";
                }

                return $"No problem at all! Is there anything else you'd like to discuss next, or are we all set for today?";
            }

            // --- TOPIC DETECTION & MEMORY RECALL ---
            string detectedTopic = DetermineTopic(cleanInput);
            if (!string.IsNullOrEmpty(detectedTopic))
            {
                // Reset tip history if switching to a new topic
                if (lastTopic != detectedTopic) usedTips.Clear();
                lastTopic = detectedTopic;

                // Track that the user has interacted with this specific topic
                if (!completedTopics.Contains(detectedTopic)) completedTopics.Add(detectedTopic);

                // Recall Check: Logic for remembering and acknowledging the user's favorite interest
                if (string.IsNullOrEmpty(FavoriteTopic))
                {
                    FavoriteTopic = detectedTopic;
                    return $"I've noted that you're interested in {FavoriteTopic}, {UserName}! {topicDefinitions[detectedTopic]}\n\nWould you like a local tip on this?";
                }
                else if (FavoriteTopic == detectedTopic)
                {
                    return $"Since you're interested in {FavoriteTopic}, you should know: {topicDefinitions[detectedTopic]}\n\nShall I give you another tip?";
                }

                return $"Certainly, {UserName}. {topicDefinitions[detectedTopic]}\n\nWould you like a specific tip or example?";
            }

            // --- SENTIMENT HANDLING ---
            // If keywords like 'worried' are found, we provide empathy and an immediate helpful tip
            if (negativeKeywords.Any(word => cleanInput.Contains(word)))
            {
                return ProcessSentiment(cleanInput);
            }

            // --- CONVERSATIONAL FLOW ---
            // Triggered when the user says "yes" or "more" to get the next tip in the list
            if (continuationKeywords.Any(word => cleanInput.Contains(word)) && !string.IsNullOrEmpty(lastTopic))
            {
                return GetUniqueTipResponse();
            }

            // Fallback for unrecognized input
            return $"I'm not sure I understand, {UserName}. Could you rephrase? You can ask about Passwords, Phishing, or Scams!";
        }

        // --- HELPER METHODS ---

        // Simple loop to match user input to our specific keyword dictionary
        private string DetermineTopic(string input)
        {
            foreach (var entry in keywordMap)
            {
                if (input.Contains(entry.Key)) return entry.Value;
            }
            return "";
        }

        // Fulfills the Sentiment requirement with South African localized empathy
        private string ProcessSentiment(string cleanInput)
        {
            var empathy = new[] { "I understand.", "It's understandable to be worried—SA has high cybercrime rates.", "Don't be overwhelmed, I'm here to help." };
            string baseMsg = empathy[random.Next(empathy.Length)];

            string detected = DetermineTopic(cleanInput);
            if (!string.IsNullOrEmpty(detected))
            {
                lastTopic = detected;
                if (!completedTopics.Contains(detected)) completedTopics.Add(detected);
                return $"{baseMsg} Let's protect you right away. {GetUniqueTipResponse()}";
            }
            return $"{baseMsg} What specifically is on your mind regarding {lastTopic}?";
        }

        // Manages the shuffling of tips and ensures no repeats
        private string GetUniqueTipResponse()
        {
            List<string> allTips = GetAllTipsForTopic(lastTopic);
            var availableTips = allTips.Where(t => !usedTips.Contains(t)).ToList();

            if (availableTips.Count > 0)
            {
                string selectedTip = availableTips[random.Next(availableTips.Count)];
                usedTips.Add(selectedTip);

                // If this is the final tip for the topic, change the prompt to help the user move on
                if (usedTips.Count == allTips.Count)
                {
                    return $"Tip: {selectedTip}\n\nI've shared all my top local tips for this! What would you like to assist you with next?";
                }

                return $"Tip: {selectedTip}\n\nWould you like another one?";
            }
            return "I've shared all my current tips for this! What else can I assist you with today?";
        }

        // Data Storage: Returns a list of 2 localized tips per requested keyword
        private List<string> GetAllTipsForTopic(string topic)
        {
            switch (topic)
            {
                case "phishing":
                    return new List<string> {
                        "Be careful of 'Smishing'. Local scammers often send fake SMSes claiming to be from 'The Courier Guy' or 'SAPS' with a dodgy link.",
                        "If you get an email from your bank asking for an 'OTP' or 'linked device' approval, it's a scam. Real SA banks won't ask for this over email."
                    };
                case "passwords":
                    return new List<string> {
                        "Avoid using common SA terms like 'Springboks2023' or 'Ladysmith' as passwords. Hackers use local dictionaries to guess these.",
                        "Try a Passphrase like 'Braai-Day-Is-Best-2026!'. It's easier to remember and much harder for hackers to crack."
                    };
                case "online scams":
                    return new List<string> {
                        "Watch out for 'Advance Fee' scams on Facebook Marketplace. Never pay a deposit for a car or rental flat in SA before seeing it in person.",
                        "Beware of WhatsApp 'Investment' groups. If a 'local expert' promises to double your R500 via Bitcoin, it is 100% a scam."
                    };
                case "data privacy":
                    return new List<string> {
                        "Under POPIA, you can ask any SA company to delete your data. If you get spam calls, ask them exactly where they got your number.",
                        "Be careful on public SA social media groups. Scammers scan these for phone numbers to target you later with fake calls."
                    };
                case "safe browsing":
                    return new List<string> {
                        "Always check for the padlock and 'https' in the URL, especially when using local sites like Takealot or Zando.",
                        "Avoid using Free Public Wi-Fi at malls for banking. Hackers can set up 'fake' hotspots to watch what you are doing."
                    };
                default:
                    return new List<string> { "Stay alert and always keep your software updated!" };
            }
        }
    }
}