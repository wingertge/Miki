﻿using Miki.Cache;
using Miki.Discord.Common;
using Miki.Discord.Common.Events;
using Miki.Discord.Internal;
using Miki.Discord.Rest;
using Miki.Discord.Rest.Entities;
using Miki.Logging;
using Miki.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Discord.Rest
{
    public class DiscordApiClient
    {
		public RestClient RestClient { get; private set; }

		ICachePool cache;

		const string discordUrl = "https://discordapp.com";
		const string baseUrl = "/api/v6";
		const string cdnUrl = "https://cdn.discordapp.com/";

		readonly JsonSerializerSettings serializer;

		public DiscordApiClient(string token, ICachePool cachePool)
		{
			RestClient = new RestClient(discordUrl + baseUrl)
				.SetAuthorization("Bot", token);
			cache = cachePool;

			serializer = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore,
			};
		}

		public async Task AddGuildBanAsync(ulong guildId, ulong userId, int pruneDays = 7, string reason = null)
		{
			var cacheClient = cache.Get;
			{
				QueryString qs = new QueryString();

				if (!string.IsNullOrWhiteSpace(reason))
				{
					qs.Add("reason", reason);
				}

				if (pruneDays != 0)
				{
					qs.Add("delete-message-days", pruneDays);
				}

				await RatelimitHelper.ProcessRateLimitedAsync(
					$"guilds:{guildId}", cacheClient,
					async () =>
					{
						return await RestClient.PutAsync($"/guilds/{guildId}/bans/{userId}" + qs.Query);
					});
			}
		}

		public async Task AddGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId)
		{
			var cacheClient = cache.Get;
			{
				await RatelimitHelper.ProcessRateLimitedAsync(
				$"guilds:{guildId}", cacheClient,
				async () =>
				{
					return await RestClient.PutAsync($"/guilds/{guildId}/members/{userId}/roles/{roleId}");
				});
			}
		}

		public async Task<DiscordChannelPacket> CreateDMChannelAsync(ulong userId)
		{
			var response = await RestClient
				.PostAsync<DiscordChannelPacket>($"/users/@me/channels", $"{{ \"recipient_id\": {userId} }}");
			return response.Data;
		}

		public async Task<DiscordRolePacket> CreateGuildRoleAsync(ulong guildId, CreateRoleArgs args)
		{
			var cacheClient = cache.Get;
			{
				return (await RatelimitHelper.ProcessRateLimitedAsync(
					$"guilds:{guildId}", cacheClient,
					async () =>
					{
						return await RestClient.PostAsync<DiscordRolePacket>(
							$"/guilds/{guildId}/roles",
							JsonConvert.SerializeObject(args) ?? ""
						);
					})).Data;
			}
		}

		public async Task DeleteMessageAsync(ulong channelId, ulong messageId)
		{
			var cacheClient = cache.Get;
			{
				await RatelimitHelper.ProcessRateLimitedAsync(
					$"channels:{channelId}:delete", cacheClient,
					async () =>
					{
						return await RestClient.DeleteAsync($"/channels/{channelId}/messages/{messageId}");
					});
			}
		}

		public async Task<DiscordMessagePacket> EditMessageAsync(ulong channelId, ulong messageId, EditMessageArgs args)
		{
			var cacheClient = cache.Get;
			{
				return (await RatelimitHelper.ProcessRateLimitedAsync(
				$"channels:{channelId}", cacheClient,
				async () =>
				{
					return await RestClient
						.PatchAsync<DiscordMessagePacket>($"/channels/{channelId}/messages/{messageId}", JsonConvert.SerializeObject(args, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
				})).Data;
			}
		}

		public async Task<DiscordRolePacket> EditRoleAsync(ulong guildId, DiscordRolePacket role)
		{
			var cacheClient = cache.Get;
			{
				return (await RatelimitHelper.ProcessRateLimitedAsync(
					$"guilds:{guildId}", cacheClient,
					async () => await RestClient.PutAsync<DiscordRolePacket>(
						$"/guilds/{guildId}/roles/{role.Id}",
						JsonConvert.SerializeObject(role)
					)
				)).Data;
			}
		}

		public async Task<DiscordUserPacket> GetCurrentUserAsync()
		{
			string key = $"discord:user:self";

			var cacheClient = cache.Get;
			{
				if (await cacheClient.ExistsAsync(key))
				{
					return await cacheClient.GetAsync<DiscordUserPacket>(key);
				}

				RestResponse<DiscordUserPacket> rc = await RestClient
					.GetAsync<DiscordUserPacket>($"/users/@me");
				await cacheClient.UpsertAsync(key, rc.Data);
				return rc.Data;
			}
		}

		public async Task<DiscordChannelPacket> GetChannelAsync(ulong channelId)
		{
			string key = $"discord:channel:{channelId}";
			DiscordChannelPacket packet = null;

			var cacheClient = cache.Get;
			if (await cacheClient.ExistsAsync(key))
				{
					packet = await cacheClient.GetAsync<DiscordChannelPacket>(key);

					if (packet == null)
					{
						Log.Debug($"cache hit on '{key}', but object was invalid");
						await cacheClient.RemoveAsync(key);
						return await GetChannelAsync(channelId);
					}
				}
				else
				{
					var data = await RatelimitHelper.ProcessRateLimitedAsync(
						$"channels:{channelId}", cache.Get,
						async () => await RestClient.GetAsync<DiscordChannelPacket>($"/channels/{channelId}")
						);

					await cacheClient.UpsertAsync(key, data.Data);
					packet = data.Data;
				}
			return packet;
		}

		public async Task<List<DiscordChannelPacket>> GetChannelsAsync(ulong guildId)
		{
			string key = $"discord:guilds:{guildId}:channels";
			List<DiscordChannelPacket> packet = null;
			var cacheClient = cache.Get;
			if (await cacheClient.ExistsAsync(key))
				{
					packet = await cacheClient.GetAsync<List<DiscordChannelPacket>>(key);

					if (packet == null)
					{
						Log.Debug($"cache hit on '{key}', but object was invalid");
						await cacheClient.RemoveAsync(key);
						return await GetChannelsAsync(guildId);
					}
				}
				else
				{
					var data = await RatelimitHelper.ProcessRateLimitedAsync(
						$"guilds:{guildId}", cacheClient,
						async () =>
						{
							return await RestClient.GetAsync<List<DiscordChannelPacket>>($"/guild/{guildId}/channels");
						});

					await cacheClient.UpsertAsync(key, data.Data);
					packet = data.Data;
				}
			return packet;
		}

		public async Task<DiscordGuildPacket> GetGuildAsync(ulong guildId)
		{
			string key = $"discord:guild:{guildId}";
			var cacheClient = cache.Get;
			
				if (await cacheClient.ExistsAsync(key))
				{
					var packet = await cacheClient.GetAsync<DiscordGuildPacket>(key);

					if (packet == null)
					{
						Log.Debug($"cache hit on '{key}', but object was invalid");
						await cacheClient.RemoveAsync(key);
						return await GetGuildAsync(guildId);
					}
					return packet;
				}
				else
				{
					var data = await RatelimitHelper.ProcessRateLimitedAsync(
						$"guilds:{guildId}", cacheClient,
						async () =>
						{
							return await RestClient.GetAsync<DiscordGuildPacket>($"/guilds/{guildId}");
						});

					await cacheClient.UpsertAsync(key, data.Data);
					return data.Data;
				}
			
		}

		public async Task<DiscordGuildMemberPacket> GetGuildUserAsync(ulong userId, ulong guildId)
		{
			string key = $"discord:guild:{guildId}:user:{userId}";

			DiscordGuildMemberPacket packet = null;
			var cacheClient = cache.Get;
			{
				if (await cacheClient.ExistsAsync(key))
				{
					packet = await cacheClient.GetAsync<DiscordGuildMemberPacket>(key);

					if (packet == null)
					{
						Log.Debug($"cache hit on '{key}', but object was invalid");
						await cacheClient.RemoveAsync(key);
						return await GetGuildUserAsync(userId, guildId);
					}
				}
				else
				{
					var rc = await RatelimitHelper.ProcessRateLimitedAsync(
						$"guilds:{guildId}", cacheClient,
						async () =>
						{
							return await RestClient.GetAsync<DiscordGuildMemberPacket>($"/guilds/{guildId}/members/{userId}");
						});

					packet = rc.Data;
					await cacheClient.UpsertAsync(key, rc.Data);
				}

				packet.User = await GetUserAsync(userId);
				packet.GuildId = guildId;
				packet.UserId = userId;
			}
			return packet;
		}

		public async Task<List<DiscordRolePacket>> GetRolesAsync(ulong guildId)
		{
			DiscordGuildPacket guild = await GetGuildAsync(guildId);

			if (guild != null)
			{
				return guild.Roles;
			}
			return null;
		}

		public async Task<DiscordUserPacket> GetUserAsync(ulong userId)
		{
			string key = $"discord:user:{userId}";
			DiscordUserPacket packet = null;
			var cacheClient = cache.Get;
			{
				if (await cacheClient.ExistsAsync(key))
				{
					packet = await cacheClient.GetAsync<DiscordUserPacket>(key);
					if (packet == null)
					{
						Log.Debug($"cache hit on '{key}', but object was invalid");
						await cacheClient.RemoveAsync(key);
						packet = await GetUserAsync(userId);
					}
				}
				else
				{
					RestResponse<DiscordUserPacket> rc = await RestClient
						.GetAsync<DiscordUserPacket>($"/users/{userId}");
					await cacheClient.UpsertAsync(key, rc.Data);
					packet = rc.Data;
				}
			}
			return packet;
		}

		public string GetUserAvatarUrl(ulong id, string hash)
		{
			return $"{cdnUrl}avatars/{id}/{hash}.png";
		}

		private bool IsRatelimited(Ratelimit rl)
		{
			return (rl?.Remaining ?? 1) <= 0 && DateTime.UtcNow <= DateTimeOffset.FromUnixTimeSeconds(rl?.Reset ?? 0);
		}

		public async Task ModifyGuildMemberAsync(ulong guildId, ulong userId, ModifyGuildMemberArgs packet)
		{
			var cacheClient = cache.Get;
			{
				await RatelimitHelper.ProcessRateLimitedAsync(
				$"guilds:{guildId}", cacheClient,
				async () =>
				{
					return await RestClient.PatchAsync($"/guilds/{guildId}/members/{userId}",
						JsonConvert.SerializeObject(packet, serializer)
					);
				});
			}
		}

		public async Task RemoveGuildBanAsync(ulong guildId, ulong userId)
		{
			var cacheClient = cache.Get;
			{
				await RatelimitHelper.ProcessRateLimitedAsync(
					$"guilds:{guildId}", cacheClient,
					async () => await RestClient.DeleteAsync($"/guilds/{guildId}/bans/{userId}")
				);
			}
		}

		public async Task RemoveGuildMemberAsync(ulong guildId, ulong userId)
		{
			var cacheClient = cache.Get;
			{
				await RatelimitHelper.ProcessRateLimitedAsync(
					$"guilds:{guildId}", cacheClient,
					async () => await RestClient.DeleteAsync($"/guilds/{guildId}/members/{userId}")
				);
			}
		}

		public async Task RemoveGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId)
		{
			var cacheClient = cache.Get;
			{
				await RatelimitHelper.ProcessRateLimitedAsync(
					$"guilds:{guildId}", cacheClient,
					async () =>
					{
						var rc = await RestClient.DeleteAsync($"/guilds/{guildId}/members/{userId}/roles/{roleId}");
						return rc;
					});
			}
		}


		public async Task<DiscordMessagePacket> SendFileAsync(ulong channelId, Stream stream, string fileName, MessageArgs args, bool toChannel = true)
		{
			args.embed = new DiscordEmbed
			{
				Image = new EmbedImage
				{
					Url = "attachment://" + fileName
				}
			};

			string json = JsonConvert.SerializeObject(args, serializer);

			List<MultiformItem> items = new List<MultiformItem>();

			var content = new StringContent(args.content);
			items.Add(new MultiformItem { Name = "content", Content = content });

			if (stream.CanSeek)
			{
				var memoryStream = new MemoryStream();
				await stream.CopyToAsync(memoryStream).ConfigureAwait(false);
				memoryStream.Position = 0;
				stream = memoryStream;
			}

			var image = new StreamContent(stream);
			items.Add(new MultiformItem { Name = "file", Content = image, FileName = fileName });
			image.Headers.Add("Content-Type", "image/png");

			image.Headers.Add("Content-Disposition", "form-data; name=\"file\"; filename=\"" + fileName + "\"");

			var cacheClient = cache.Get;
			{
				await RatelimitHelper.ProcessRateLimitedAsync(
				$"channels:{channelId}",
				cacheClient, async () =>
				{
					RestResponse rc = await RestClient
						.PostMultipartAsync($"/channels/{channelId}/messages",
						items.ToArray()
					);
					return rc;
				});
			}
			// TODO: fix returns
			return null;
		}

		public async Task<DiscordMessagePacket> SendMessageAsync(ulong channelId, MessageArgs args, bool toChannel = true)
		{
			string json = JsonConvert.SerializeObject(args, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
			var cacheClient = cache.Get;
			{
				return (await RatelimitHelper.ProcessRateLimitedAsync(
				$"channels:{channelId}",
				cacheClient, async () =>
				{
					return await RestClient.PostAsync<DiscordMessagePacket>($"/channels/{channelId}/messages", json);
				})).Data;
			}
		}
	}
}
