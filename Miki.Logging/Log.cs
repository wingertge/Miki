using System;
using System.Collections.Generic;
using System.IO;

namespace Miki.Logging
{
	public static class Log
	{
		public static event Action<string, LogLevel> OnLog;

		public static LogTheme Theme = new LogTheme();

		/// <summary>
		/// Display a debug message
		/// </summary>
		/// <param name="message">information about the action</param>
		public static void Debug(string message)
		{
			WriteToLog("[dbug] " + message, LogLevel.Debug);
		}

		/// <summary>
		/// Display a error message.
		/// </summary>
		/// <param name="message">information about the action</param>
		public static void Error(string message)
		{
			WriteToLog("[crit] " + message, LogLevel.Error);
		}
		public static void Error(Exception exception)
		{
			Error("[crit]" + exception.ToString());
		}

		/// <summary>
		/// Display a standard message.
		/// </summary>
		/// <param name="message">information about the action</param>
		public static void Message(string message)
		{
			WriteToLog("[info] " + message, LogLevel.Information);
		}

		/// <summary>
		/// Display a trace message
		/// </summary>
		/// <param name="message">information about the action</param>
		public static void Trace(string message)
		{
			WriteToLog("[trce] " + message, LogLevel.Verbose);
		}

		/// <summary>
		/// Display a warning message.
		/// </summary>
		/// <param name="message">information about the action</param>
		public static void Warning(string message)
		{
			WriteToLog("[warn] " + message, LogLevel.Warning);
		}

		/// <summary>
		/// Display a warning message.
		/// </summary>
		/// <param name="message">information about the action</param>
		public static void WarningAt(string tag, string message)
		{
			WriteToLog($"[warn@{tag}] {message}", LogLevel.Warning);
		}

		private static void WriteToLog(string message, LogLevel level)
		{
			LogColor color = Theme.GetColor(level);

			Console.ForegroundColor = color.Foreground ?? ConsoleColor.White;
			Console.BackgroundColor = color.Background ?? ConsoleColor.Black;

			OnLog?.Invoke(message, level);

			Console.ResetColor();
		}
	}
}