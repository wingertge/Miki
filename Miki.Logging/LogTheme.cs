using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Logging
{
	public class LogTheme
	{
		Dictionary<LogLevel, LogColor> colors = new Dictionary<LogLevel, LogColor>();

		public LogColor GetColor(LogLevel level)
		{
			if (colors.TryGetValue(level, out LogColor value))
			{
				return value;
			}
			return new LogColor();
		}

		public void SetColor(LogLevel level, LogColor color)
		{
			if (colors.ContainsKey(level))
			{
				colors[level] = color;
				return;
			}
			colors.Add(level, color);
		}
	}
}
