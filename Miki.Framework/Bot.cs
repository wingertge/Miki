using Miki.Framework.Events;
using Miki.Framework.FileHandling;
using System;
using System.IO;
using System.Threading.Tasks;
using Miki.Common;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Miki.Discord;
using Miki.Discord.Caching;
using Miki.Cache;
using Miki.Scaffold;

namespace Miki.Framework
{
    public class Bot
    {
		//public static Bot Instance { get; private set; } //containerise

		public IChatClient ChatClient { get; private set; }
		public ICacheClient CacheClient { get; private set; }

		public Secrets Information { private set; get; }

		private List<IAttachable> attachables = new List<IAttachable>();

		// TODO: rework params
		public Bot(int shardCount, IChatClient chatClient, ICacheClient cacheClient, Secrets secrets)
        {
			Information = secrets;
			CacheClient = cacheClient;
            ChatClient = chatClient;
            /*TODO: Implement elsewhere
			ChatClient = new DiscordClient(new DiscordClientConfigurations
			{
				Pool = cachePool,
				RabbitMQExchangeName = "consumer",
				RabbitMQQueueName = "gateway",
				RabbitMQUri = rabbitUrl,
				Token = cInfo.Token
			});
            */

            /*TODO: Implement elsewhere
			CacheClient = new CacheClient(
				ChatClient._websocketClient,
				cachePool, ChatClient._apiClient
			);*/
		}

		public void Attach(IAttachable attachable)
		{
			attachables.Add(attachable);
			attachable.AttachTo(this);
		}

		public T GetAttachedObject<T>() where T : class, IAttachable
		{
			for(int i = 0; i < attachables.Count; i++)
			{
				if(attachables[i] is T)
				{
					return attachables[i] as T;
				}
			}
			return default(T);
		}
	}
}