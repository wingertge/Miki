using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.API.GameService
{
	class SlotsService : IGameService
	{
		public IGameResponse Run()
		{
			return new SlotsResponse()
			{
				Bet = 100,
				Gain = 100
			};
		} 
	}
}
