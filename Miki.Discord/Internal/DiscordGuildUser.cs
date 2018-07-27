﻿using Miki.Discord.Common;
using Miki.Discord.Rest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Discord.Internal
{
	public class DiscordGuildUser : IDiscordGuildUser
    {
		DiscordGuildMemberPacket _packet;
		DiscordClient _client;

		public DiscordGuildUser(DiscordGuildMemberPacket packet, DiscordClient client)
		{
			_packet = packet;
			_client = client;
		}

		public ulong Id
			=> _packet.UserId;

		public string Username
			=> _packet.User.Username;

		public string Discriminator
			=> _packet.User.Discriminator;

		public bool IsBot
			=> _packet.User.IsBot;

		public string AvatarId
			=> _packet.User.Avatar; 

		public string Mention
			=> $"<@{Id}>";

		public string Nickname 
			=> _packet.Nickname;

		public IReadOnlyCollection<ulong> RoleIds 
			=> _packet.Roles;

		public ulong GuildId 
			=> _packet.GuildId;

		public DateTimeOffset JoinedAt
			=> new DateTimeOffset();

		public int Hierarchy
		{
			get
			{
				if (RoleIds != null && RoleIds.Count > 0)
				{
					return _client.GetRolesAsync(GuildId).Result
					  .Where(x => RoleIds?.Contains(x.Id) ?? false)
					  .Max(x => x.Position);
				}
				return 0;
			}
		}

		public DateTimeOffset CreatedAt => throw new NotImplementedException();

		public async Task AddRoleAsync(IDiscordRole role)
			=> await _client.AddGuildMemberRoleAsync(GuildId, Id, role.Id);

		public string GetAvatarUrl()
			=>	_client.GetUserAvatarUrl(_packet.UserId, _packet.User.Avatar);

		public async Task<IDiscordChannel> GetDMChannel()
			=> await _client.CreateDMAsync(_packet.UserId);


		public async Task<IDiscordGuild> GetGuildAsync()
			=> await _client.GetGuildAsync(_packet.GuildId);

		public async Task KickAsync(string reason = "")
			=> await _client.RemoveGuildMemberAsync(GuildId, Id);

		public async Task RemoveRoleAsync(IDiscordRole role)
			=> await _client.RemoveGuildMemberRoleAsync(GuildId, Id, role.Id);

	}
}
