using System;
using System.Collections.Generic;
using System.Linq;
namespace Miki.API.GameService
{
	public class GameInformation
	{
		public int bet;
		public int gain;

		public int BetAmount
		{
			get { return bet; }
		}

		public int Gain
		{
			get { return gain; }
		}
	}

	public class SlotsInformation : GameInformation
	{
		public List<string> picks;

		public List<string> Picks
		{
			get { return picks; }
		}
	}

	public class CoinTossInformation : GameInformation
	{
		public string player;
		public string computer;

		public string PlayerSide
		{
			get { return player; }
		}

		public string ComputerSide
		{
			get { return computer; }
		}

		public string HeadsImageUrl
		{
			get { return "https://miki.ai/assets/img/miki-default-heads.png"; }
		}

		public string HeadsBonusImageUrl
		{
			get { return "https://miki.ai/assets/img/miki-secret-heads.png"; }
		}

		public string TailsImageUrl
		{
			get { return "https://miki.ai/assets/img/miki-default-tails.png"; }
		}

		public string TailsBonusImageUrl
		{
			get { return "https://miki.ai/assets/img/miki-secret-tails.png"; }
		}
	}
}
