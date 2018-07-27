using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Miki.Patreon.Types
{
	public enum PatreonType
	{
		PLEDGE, USER, CAMPAIGN, REWARD, GOAL
	}

	public class PatreonPledge
	{
		[JsonProperty("data")]
		public PatreonEntity Data { get; set; }

		[JsonProperty("included")]
		public List<PatreonEntity> Included { get; set; } = new List<PatreonEntity>();

		[JsonProperty("relationships")]
		public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();

		//[JsonProperty("meta")]
		//public Dictionary<string, object> Meta { get; set; } = new Dictionary<string, object>();
	}

	public class PatreonEntity
	{
		[JsonProperty("attributes")]
		public JObject Attributes { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("relationships")]
		public JObject Relationships { get; set; }

		[JsonProperty("type")]
		public PatreonType Type { get; set; }
	}

	public class PatreonCampaign : PatreonEntity
	{
		[JsonProperty("attributes")]
		public CampaignAttribute Attributes { get; set; }
	}

	public class PledgeAttribute
	{
		[JsonProperty("amount_cents")]
		public int AmountCents { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		[JsonProperty("pledge_cap_cents")]
		public int? PatronPledgeCapCents { get; set; }

		[JsonProperty("patron_pays_fees")]
		public bool PatreonPaidFeed { get; set; }

		[JsonProperty("declined_since")]
		public DateTime? DeclinedSince { get; set; }
	}

	public class UserAttribute
	{
		[JsonProperty("about")]
		public string About { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		[JsonProperty("discord_id")]
		public string DiscordUserId;

		[JsonProperty("email")]
		public string EmailAddress { get; set; }

		[JsonProperty("facebook")]
		public string FacebookUrl { get; set; }

		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("full_name")]
		public string FullName { get; set; }

		[JsonProperty("gender")]
		public int Gender { get; set; }

		[JsonProperty("image_url")]
		public string ImageUrl { get; set; }

		[JsonProperty("is_email_verified")]
		public bool IsEmailVerified { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		//[JsonProperty("social_connections")]
		//public Dictionary<string, Dictionary<string, string>> SocialConnections { get; set; } = new Dictionary<string, Dictionary<string, string>>();

		[JsonProperty("thumb_url")]
		public string ThumbnailUrl { get; set; }

		[JsonProperty("twitch")]
		public string TwitchUrl { get; set; }

		[JsonProperty("twitter")]
		public string TwitterUrl { get; set; }

		[JsonProperty("url")]
		public string PatreonUrl { get; set; }

		[JsonProperty("vanity")]
		public string Vanity { get; set; }

		[JsonProperty("youtube")]
		public string YoutubeUrl { get; set; }
	}

	public class CampaignAttribute
	{
		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		[JsonProperty("creation_count")]
		public int CreationCount { get; set; }

		[JsonProperty("creation_name")]
		public string CreationName { get; set; }

		[JsonProperty("discord_server_id")]
		public string DiscordServerId { get; set; }

		[JsonProperty("display_patron_goals")]
		public bool DisplayPatreonGoals { get; set; }

		[JsonProperty("earnings_visibility")]
		public string EarningsVisibility { get; set; }

		[JsonProperty("image_small_url")]
		public string SmallImageUrl { get; set; }

		[JsonProperty("image_url")]
		public string ImageUrl { get; set; }

		[JsonProperty("is_charged_immediately")]
		public bool ChargedImmediately { get; set; }

		[JsonProperty("is_monthly")]
		public bool ChargedMonthly { get; set; }

		[JsonProperty("is_nsfw")]
		public bool IsNsfw { get; set; }

		[JsonProperty("is_plural")]
		public bool IsPlural { get; set; }

		[JsonProperty("main_video_embed")]
		public string MainVideoEmbed { get; set; }

		[JsonProperty("main_video_url")]
		public string MainVideoUrl { get; set; }

		[JsonProperty("one_liner")]
		public string OneLiner { get; set; }

		[JsonProperty("outstanding_payment_amount_cents")]
		public int OutstandingPaymentAmountCents { get; set; }

		[JsonProperty("patron_count")]
		public int PatronCount { get; set; }

		[JsonProperty("pay_per_name")]
		public string PayPerName { get; set; }

		[JsonProperty("pledge_sum")]
		public int PledgeSum { get; set; }

		[JsonProperty("pledge_url")]
		public string PledgeUrl { get; set; }

		[JsonProperty("published_at")]
		public DateTime PublishedAt { get; set; }

		[JsonProperty("summary")]
		public string Summary { get; set; }

		[JsonProperty("thanks_embed")]
		public string ThanksEmbed { get; set; }

		[JsonProperty("thanks_msg")]
		public string ThanksMessage { get; set; }

		[JsonProperty("thanks_video_url")]
		public string ThanksVideoUrl { get; set; }
	}

	public class PatreonRelationships
	{
		RelationshipItem Address { get; set; }
		RelationshipItem Card { get; set; }
		RelationshipItem Creator { get; set; }
		RelationshipItem Patron { get; set; }
		RelationshipItem Reward { get; set; }

		List<PatreonEntity> Goals { get; set; }
		List<PatreonEntity> Rewards { get; set; }
	}

	public class RelationshipItem
	{
		[JsonProperty("data")]
		public PatreonEntity Data { get; set; }

		[JsonProperty("links")]
		public PatreonLinks Links { get; set; }
	}

	public class PatreonLinks
	{
		string self;
		string related;
	}

	public class PatreonReward
	{
	}

	public class PatreonAddress
	{
	}

	public class PatreonCard
	{
	}
}
