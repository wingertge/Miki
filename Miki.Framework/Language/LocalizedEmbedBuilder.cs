﻿using Miki.Discord;
using Miki.Discord.Common;
using Miki.Discord.Rest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Framework.Language
{
    public class LocalizedEmbedBuilder
	{
		public EmbedBuilder EmbedBuilder { get; private set; } = new EmbedBuilder();
		private ulong channelId;

		public LocalizedEmbedBuilder(ulong channelId)
		{
			this.channelId = channelId;
		}

		public LocalizedEmbedBuilder AddField(IResource title, IResource content, bool inline = false)
		{
			EmbedBuilder.AddField(title.Get(channelId), content.Get(channelId), inline);
			return this;
		}

		public DiscordEmbed Build()
			=> EmbedBuilder.ToEmbed();

		public LocalizedEmbedBuilder WithAuthor(IResource title, string iconUrl = null, string url = null)
		{
			EmbedBuilder.SetAuthor(title.Get(channelId), iconUrl, url);
			return this;
		}

		public LocalizedEmbedBuilder WithColor(Color color)
		{
			EmbedBuilder.SetColor(color);
			return this;
		}

		public LocalizedEmbedBuilder WithDescription(string description, params object[] param)
			=> WithDescription(new LanguageResource(description, param));
		public LocalizedEmbedBuilder WithDescription(LanguageResource description)
		{
			EmbedBuilder.SetDescription(description.Get(channelId));
			return this;
		}

		public LocalizedEmbedBuilder WithFooter(string text, string iconUrl = null, params object[] param)
			=> WithFooter(new LanguageResource(text, param), iconUrl);
		public LocalizedEmbedBuilder WithFooter(IResource text, string iconUrl = null)
		{
			EmbedBuilder.SetFooter(text.Get(channelId), iconUrl);
			return this;
		}

		public LocalizedEmbedBuilder WithImageUrl(string imageUrl)
		{
			EmbedBuilder.SetImage(imageUrl);
			return this;
		}

		public LocalizedEmbedBuilder WithThumbnailUrl(string thumbnailUrl)
		{
			EmbedBuilder.SetThumbnail(thumbnailUrl);
			return this;
		}

		public LocalizedEmbedBuilder WithTitle(string resource, params object[] param)
			=> WithTitle(new LanguageResource(resource, param));
		public LocalizedEmbedBuilder WithTitle(IResource title)
		{
			EmbedBuilder.SetTitle(title.Get(channelId));
			return this;
		}
    }
}
