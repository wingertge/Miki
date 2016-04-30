using System;

namespace Miki.Core.Debug
{
    internal class Log
    {
        /// <summary>
        /// Display an info message in the console.
        /// </summary>
        /// <param name="message">information about the action</param>
        public static void Message(string message)
        {
            Console.WriteLine("INFO: " + message);
        }

        /// <summary>
        /// Display an error message in the console.
        /// </summary>
        /// <param name="message">information about the action</param>
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Display an warning message in the console.
        /// </summary>
        /// <param name="message">information about the action</param>
        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("WARNING: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Display a message when something is done.
        /// </summary>
        /// <param name="message">information about the action</param>
        public static void Done(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DONE: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}