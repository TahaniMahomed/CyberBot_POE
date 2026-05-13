using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Media;

namespace CyberBot_POE
{
    public partial class MainWindow : Window
    {
        // Link to your chatbot class (ensure the lowercase 'c' matches your file)
        private chatbot myBot = new chatbot();

        public MainWindow()
        {
            InitializeComponent();
            LoadTask1Requirements();
        }

        private void LoadTask1Requirements()
        {
            // ASCII Art for ShieldBot
            AsciiDisplay.Text = @"
     _____ _    _ _____ ______ _      _____  ____   _______ 
    / ____| |  | |_   _|  ____| |    |  __ \|  _ \ / ______|
   | (___ | |__| | | | | |__  | |    | |  | | |_) | |      
    \___ \|  __  | | | |  __| | |    | |  | |  _ <| |      
    ____) | |  | |_| |_| |____| |____| |__| | |_) | |____  
   |_____/|_|  |_|_____|______|______|_____/|____/ \______|";

            // Voice Welcome logic
            try
            {
                SoundPlayer player = new SoundPlayer("welcome2.wav");
                player.Play();
            }
            catch { /* Silent fallback if audio file isn't in debug folder */ }

            AddMessage("ShieldBot", "Hello! I am your AI Cybersecurity Assistant. What is your name?", Colors.Cyan);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            HandleInput();
        }

        private void UserInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) HandleInput();
        }

        private void HandleInput()
        {
            string input = UserInputBox.Text;
            if (string.IsNullOrWhiteSpace(input)) return;

            // Display User Message
            AddMessage("You", input, Colors.White);
            UserInputBox.Clear();

            // GET RESPONSE FROM BOT ENGINE
            // This calls the logic we refined in the chatbot class
            string response = myBot.HandleUserQuery(input);

            // Display Bot Message
            AddMessage("ShieldBot", response, Colors.Cyan);
        }

        public void AddMessage(string sender, string text, Color color)
        {
            TextBlock msg = new TextBlock();
            msg.Text = $"{sender}: {text}";
            msg.Foreground = new SolidColorBrush(color);
            msg.TextWrapping = TextWrapping.Wrap;
            msg.Margin = new Thickness(0, 5, 0, 5);
            msg.FontSize = 14;

            ChatContainer.Children.Add(msg);

            // Auto-scroll logic
            if (VisualTreeHelper.GetParent(ChatContainer) is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToEnd();
            }
        }
    }
}