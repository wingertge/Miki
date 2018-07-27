using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Miki.Discord.Rest;
using Miki.Discord.Rest.Entities;
using Newtonsoft.Json;

namespace Miki.WebAPI.Discord.Controllers
{
	[BasicAuthentication]
	[Route("api")]
	public class DiscordController : Controller
	{
		internal static DiscordClient client;

		[HttpPost("users/{userid}/messages")]
		public async Task<IActionResult> PostUserMessage(ulong userid, [FromBody]MessageArgs message)
		{
			DiscordChannel channel = await client.CreateDMAsync(userid);

			if (channel == null)
			{
				return new NotFoundResult();
			}

			return await PostChannelMessage(channel.Id, message);
		}

		[HttpPost("channels/{channelid}/messages")]
		public async Task<IActionResult> PostChannelMessage(ulong channelid, [FromBody]MessageArgs message)
		{
			await client.SendMessageAsync(channelid, message);
			return new OkResult();
		}
	}
}
