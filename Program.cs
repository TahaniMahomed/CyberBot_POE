using System;

namespace CyberBot_POE
{
    /// <summary>
    /// The entry point of the application. 
    /// Now configured to launch the WPF GUI instead of the Console.
    /// </summary>
    public class Program
    {
        [STAThread] // This attribute is MANDATORY for launching a WPF Window
        public static void Main()
        {
            // We initialize the WPF application engine
            var app = new System.Windows.Application();

            // We tell the engine to start and open your MainWindow
            app.Run(new MainWindow());
        }
    }
}