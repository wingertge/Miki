using IA;

using System.Net;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json.Linq;

namespace Miki.API.GameService
{
	class GameService
	{
		private static GameService _instance = new GameService();
		public static GameService Instance => _instance;

		private string baseUrl = "http://localhost:8000/";

		public void SetBaseUrl( string _baseUrl )
		{

			baseUrl = _baseUrl;
		}

		public async Task<SlotsInformation> PlaySlots(int betAmount)
		{
			return await RequestAsync($"slot_machine/{betAmount}");
		}

		public async Task<dynamic> RequestAsync(string path)
		{
			RestClient client = new RestClient(baseUrl + path);
			RestRequest request = new RestRequest(Method.GET);
			IRestResponse restResponse = (await client.ExecuteGetTaskAsync(request));

			if(restResponse.ErrorException != null || restResponse.StatusCode != HttpStatusCode.OK)
			{
				throw new GameResultException("The request to GameService was unsuccessful!");
			}

			try
			{
				GameResponse<SlotsInformation> response = new GameResponse<SlotsInformation>(JObject.Parse(restResponse.Content));
				return response.result.GameInfo;
			}
			catch(GameResultException e)
			{
				Log.Error(e.Message);
				throw;
			}
		}
	}
}
