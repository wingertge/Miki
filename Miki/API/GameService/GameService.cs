using IA;

using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json.Linq;

namespace Miki.API.GameService
{
	class GameService
	{
		private static GameService _instance = new GameService();
		public static GameService Instance => _instance;

		public Dictionary<int, IGameService> gameServices = new Dictionary<int, IGameService>();

		public bool AddService<T>() where T : IGameService
		{
			int hash = typeof(T).GetHashCode();

			IGameService gameService = Activator.CreateInstance<T>() as IGameService;

			if(!gameServices.ContainsKey(hash))
			{
				gameServices.Add(hash, gameService);
			}
			return false;
		}

		public IGameResponse RunService<T>()
			where T : IGameService
		{
			return gameServices[typeof(T).GetHashCode()].Run();
		}

		public U RunServiceEx<T, U>()
			where T : IGameService
			where U : IGameResponse
		{
			return (U)RunService<T>();
		}

		private string baseUrl = "http://localhost:8000/";

		public void SetBaseUrl( string _baseUrl )
		{

			baseUrl = _baseUrl;
		}

		//public async Task<SlotsInformation> PlaySlots(int betAmount)
		//{
		//	return await RequestAsync($"slot_machine/{betAmount}");
		//}

		//public async Task<dynamic> RequestAsync(string path)
		//{
		//	RestClient client = new RestClient(baseUrl + path);
		//	RestRequest request = new RestRequest(Method.GET);
		//	IRestResponse restResponse = (await client.ExecuteGetTaskAsync(request));

		//	if(restResponse.ErrorException != null || restResponse.StatusCode != HttpStatusCode.OK)
		//	{
		//		throw new GameResultException("The request to GameService was unsuccessful!");
		//	}

		//	try
		//	{
		//		GameResponse<SlotsInformation> response = new GameResponse<SlotsInformation>(JObject.Parse(restResponse.Content));
		//		return response.result.GameInfo;
		//	}
		//	catch(GameResultException e)
		//	{
		//		Log.Error(e.Message);
		//		throw;
		//	}
		//}
	}
}
