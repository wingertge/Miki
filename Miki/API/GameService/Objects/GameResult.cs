using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Miki.API.GameService
{
	public class GameResult<T> where T : GameInformation
	{
		private bool success = true;
		private T gameInformation;

		public bool Success
		{
			get { return success; }
		}

		public T GameInfo
		{
			get { return gameInformation; }
		}

		public GameResult(JToken rToken)
		{
			if(rToken["Err"] != null)
			{
				throw new GameResultException((string)rToken["Err"]);
			}
			gameInformation = rToken["Ok"].ToObject<T>();
		}
	}

	[Serializable]
	public class GameResultException : Exception
	{
		public GameResultException() { }
		public GameResultException(string message) : base(message) { }
		public GameResultException(string message, Exception inner) : base(message, inner) { }
		protected GameResultException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
