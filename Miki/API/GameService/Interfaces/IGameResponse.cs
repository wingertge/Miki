using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Miki.API.GameService
{
	public interface IGameResponse
	{
		[JsonProperty("bet")]
		int Bet { get; set; }

		[JsonProperty("gain")]
		int Gain { get; set; }
	}
}
