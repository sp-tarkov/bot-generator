using System;

namespace Generator.Helpers
{
    public static class LoggingHelpers
    {
        internal static string LogTimeTaken(double totalSeconds)
        {
            return Math.Round(totalSeconds, 2, MidpointRounding.ToEven).ToString();
        }

        public static void LogToConsole(string message)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine(message);

            ResetConsoleColours();
        }

        private static void ResetConsoleColours()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
