using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IA;
using IA.Events;
using IA.Events.Attributes;
using IA.SDK;
using IA.SDK.Events;
using IA.SDK.Interfaces;

using Miki.Models;
using Miki.Languages;

using Miki.API.GameService;

namespace Miki.Modules.Gambling
{
	[Module("GamblingNew")]
	class GamesModuleNew
	{
		GameService gameService = GameService.Instance;

		public GamesModuleNew()
		{
			// TODO: Make it so this is loaded from a config file.
			gameService.SetBaseUrl("http://localhost:8000/");
		}

		[Command(Name = "slotsnew")]
		public async Task SlotsAsync(EventContext e)
		{
			await ValidateBet(e, StartSlots, 99999);
		}

		public async Task StartSlots(EventContext e, int bet)
		{
			GameService gameService = GameService.Instance;
			SlotsInformation game;

			try
			{
				game = await gameService.PlaySlots(bet);
			}
			catch(GameResultException exception)
			{
				await Utils.ErrorEmbed(e, exception.Message).SendToChannel(e.Channel);
				return;
			}

			if(game != null)
			{
				IDiscordEmbed embed = Utils.Embed
				.SetAuthor(e.GetResource(Locale.SlotsHeader) + " | " + e.Author.Username, e.Author.AvatarUrl, "https://patreon.com/mikibot")
				.SetDescription(string.Join("", game.Picks));

				using(MikiContext context = new MikiContext())
				{
					User user = await context.Users.FindAsync(e.Author.Id.ToDbLong());

					if(game.Gain <= 0)
					{
						embed.AddField(
							e.GetResource("miki_module_fun_slots_lose_header"),
							e.GetResource("miki_module_fun_slots_lose_amount", bet, user.Currency - bet)
						);
					}
					else
					{
						embed.AddField(
							e.GetResource(Locale.SlotsWinHeader),
							e.GetResource(Locale.SlotsWinMessage, game.Gain, user.Currency + game.Gain)
						);
					}

					await user.AddCurrencyAsync(game.Gain, e.Channel);
					await context.SaveChangesAsync();
				}

				await embed.SendToChannel(e.Channel);
			}
			else
			{
				Log.Error("Something went horribly wrong!");
			}
		}

		public async Task ValidateBet(EventContext e, Func<EventContext, int, Task> callback = null, int maxBet = 1000000)
		{
			if(!string.IsNullOrEmpty(e.arguments))
			{
				int bet;
				const int noAskLimit = 10000;

				using(MikiContext context = new MikiContext())
				{
					User user = await context.Users.FindAsync(e.Author.Id.ToDbLong());

					if(user == null)
					{
						// TODO: add user null error
						return;
					}

					string checkArg = e.arguments.Split(' ')[0];

					// Parse bet.
					if(checkArg.ToLower() == "all" || e.arguments == "*")
					{
						bet = user.Currency;
					}
					else if(!int.TryParse(checkArg, out bet))
					{
						await e.ErrorEmbed(e.GetResource("miki_error_gambling_parse_error")).SendToChannel(e.Channel);
						return;
					}

					// Validate bet.
					if(bet < 1)
					{
						await e.ErrorEmbed(e.GetResource("miki_error_gambling_zero_or_less")).SendToChannel(e.Channel);
						return;
					}
					else if(bet > user.Currency)
					{
						await e.ErrorEmbed(e.GetResource("miki_mekos_insufficient")).SendToChannel(e.Channel);
						return;
					}
					else if(bet > maxBet)
					{
						await e.ErrorEmbed($"You cannot bet more than `{maxBet}` mekos!").SendToChannel(e.Channel);
						return;
					}
					else if(bet > noAskLimit)
					{
						IDiscordEmbed embed = Utils.Embed;
						embed.SetDescription($"Are you sure you want to bet **{bet}**? You currently have `{user.Currency}` mekos.\n\nType `yes` to confirm.");
						embed.SetColor(new IA.SDK.Color(0.4f, 0.6f, 1f));
						await embed.SendToChannel(e.Channel);

						CommandHandler confirmCommand = new CommandHandlerBuilder(Bot.instance.Events)
						.AddPrefix("")
						.DisposeInSeconds(20)
						.SetOwner(e.message)
						.AddCommand(
							new RuntimeCommandEvent("yes").Default(async (ec) =>
						 {
							 await ec.commandHandler.RequestDisposeAsync();
							 await ec.message.DeleteAsync();
							 if(callback != null)
							 {
								 await callback(e, bet);
							 }
						 })
						).Build();

						Bot.instance.Events.AddPrivateCommandHandler(e.message, confirmCommand);
					}
					else
					{
						if(callback != null)
						{
							await callback(e, bet);
						}
					}
				}
			}
			else
			{
				await Utils.ErrorEmbed(e.Channel.GetLocale(), e.GetResource("miki_error_gambling_no_arg")).SendToChannel(e.Channel);
			}
		}
	}
}
