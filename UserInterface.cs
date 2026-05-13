using System;
using System.Threading;
using System.Media;

namespace CyberBot
{
    /// <summary>
    /// Handles all visual and audio output for the application.
    /// </summary>
    public static class UserInterface
    {
        public static void PlayGreetingAudio(string fileName)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(fileName);
                player.Play();
            }
            catch (Exception)
            {
                // Silent fallback if audio hardware is missing
                Console.WriteLine("[Audio system initialized]");
            }
        }

        /// <summary>
        /// Outputs text with a typewriter effect and specific color.
        /// </summary>
        public static void TypeLine(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(15);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void DisplayHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
    ██████╗██╗  ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗
   ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝
   ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   
   ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   
   ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝   ██║   
    ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝    ╚═╝   

                  PROTECT • DETECT • RESPOND
    ");
            Console.WriteLine("================================================================");
            Console.WriteLine("    CYBERSECURITY AWARENESS BOT - VERSION 2.0 (SOUTH AFRICA)      ");
            Console.WriteLine("================================================================");
            Console.ResetColor();
        }
    }
}