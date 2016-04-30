using DiscordSharp.Events;
using System;
using System.Linq;

namespace Miki.Core.Command.Objects
{
    internal class Roulette : Command
    {
        private Random r = new Random();

        public override void Initialize()
        {
            id = "roulette";
            appearInHelp = true;
            description = "picks a random user from all users";

            parameterType = ParameterType.BOTH;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            if (e.MessageText.Length == 1)
            {
                e.Channel.SendMessage("And the winner is: <@" + e.Channel.Parent.Members.ToList()[r.Next(0, e.Channel.Parent.Members.Count)].Value.ID + ">");
            }
            else
            {
                e.Channel.SendMessage("And the winner of '" + e.MessageText.Substring(10) + "' is: <@" + e.Channel.Parent.Members.ToList()[r.Next(0, e.Channel.Parent.Members.Count)].Value.ID + ">");
            }
            base.PlayCommand(e);
        }
    }
}