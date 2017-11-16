using Newtonsoft.Json.Linq;

namespace Miki.API.GameService
{
	public class IGameResponse<T> where T : GameInformation
	{
		public GameResult<T> result;

		public IGameResponse(JObject rObject)
		{
			result = new GameResult<T>(rObject.GetValue("status"));
		}
	}
}
