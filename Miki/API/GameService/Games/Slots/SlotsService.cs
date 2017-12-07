using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using IA;

namespace Miki.API.GameService
{
	class SlotsService : IGameService
	{
		public async Task<IGameResponse> Play<T>(T arguments)
			where T : SlotsArguments
		{
			RestResponse<string> restResponse = await new RestClient($"http://localhost:8000/slot_machine/{arguments}").GetAsync();
			JObject json = JObject.Parse( restResponse.Data );

			SlotsResponse gameResponse = new SlotsResponse
			{
				Bet = (int)json["status"]["Ok"]["bet"],
				Gain = (int)json["status"]["Ok"]["gain"]
			};

			return gameResponse;
		} 
	}

	public interface SlotsArguments : IGameArguments
	{
		int Bet { get; set; }
	}
}
