﻿using Miki.Common.Builders;
using Miki.Discord;
using Miki.Discord.Common;
using Miki.Discord.Rest;
using Miki.Discord.Rest.Entities;
using Miki.Framework;
using Miki.Framework.Languages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Discord
{
    public static class DiscordExtensions
    {
		public static EmbedBuilder AddInlineField(this EmbedBuilder b, string header, object value)
			=>	b.AddField(header, value.ToString(), true);

		public static EmbedBuilder AddInlineField(this EmbedBuilder b, string resourceHeader, string resourceValue, ulong channelId)
			=> b.AddInlineField(Locale.GetString(channelId, resourceHeader), Locale.GetString(channelId, resourceValue));

		public static void QueueToChannel(this DiscordEmbed embed, IDiscordChannel channel)
		{
			MessageBucket.Add(new MessageBucketArgs()
			{
				properties = new MessageArgs()
				{
					embed = embed
				},
				channel = channel
			});
		}

		public static async Task<IDiscordMessage> SendToChannel(this DiscordEmbed embed, IDiscordChannel channel)
		{
			//if (!(await (await (channel as IDiscordGuildChannel).GetGuildAsync()).GetSelfAsync())
			//	.GuildPermissions.HasFlag (GuildPermission.EmbedLinks))
			//{
			//	if (!string.IsNullOrEmpty(embed.Image?.Url ?? ""))
			//	{
			//		return await channel.SendMessageAsync(embed.ToMessageBuilder().Build());
			//	}

			//	using (WebClient wc = new WebClient())
			//	{
			//		byte[] image = wc.DownloadData(embed.Image.Url);
			//		using (MemoryStream ms = new MemoryStream(image))
			//		{
			//			return await channel.SendFileAsync(ms, embed.ToMessageBuilder().Build());
			//		}
			//	}
			//}
			return await channel.SendMessageAsync("", false, embed);
		}
		public static async Task<IDiscordMessage> SendToUser(this DiscordEmbed embed, IDiscordUser user)
		{
			await Task.Yield();
			return await (await user.GetDMChannel()).SendMessageAsync("", false, embed);
		}

		public static void QueueMessageAsync(this IDiscordChannel channel, string message)
			=> MessageBucket.Add(new MessageBucketArgs()
			{
				properties = new MessageArgs()
				{
					content = message
				},
				channel = channel
			});

		public static MessageBuilder ToMessageBuilder(this DiscordEmbed embed)
		{
			MessageBuilder b = new MessageBuilder();

			if (embed.Author != null)
			{
				b.AppendText(embed.Author.Name, MessageFormatting.Bold);
			}

			b.AppendText(embed.Title, MessageFormatting.Bold)
			 .AppendText(embed.Description);

			foreach (EmbedField f in embed.Fields)
			{
				b.AppendText(f.Title, MessageFormatting.Underlined)
				 .AppendText(f.Content)
				 .NewLine();
			}

			if (embed.Footer != null)
			{
				b.AppendText(embed.Footer.Text, MessageFormatting.Italic);
			}

			return b;
		}
	}
}
