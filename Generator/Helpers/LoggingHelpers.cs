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
            Console.WriteLine(message);
        }
    }
}
