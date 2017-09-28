using Newtonsoft.Json.Linq;

namespace Miki.API.GameService
{
	public class GameResponse<T> where T : GameInformation
	{
		public GameResult<T> result;

		public GameResponse(JObject rObject)
		{
			result = new GameResult<T>(rObject.GetValue("status"));
		}
	}
}
