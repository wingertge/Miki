using DiscordSharp.Events;

namespace Miki.Core.Command.Objects
{
    public class FizzbuzzCommand : Command
    {
        public override void Initialize()
        {
            id = "fizzbuzz";
            appearInHelp = false;
            parameterType = ParameterType.YES;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            base.PlayCommand(e);
            int input = 0;
            try
            {
                input = int.Parse(message.Split(' ')[1]);
                if (input == 0)
                {
                    e.Channel.SendMessage("Nice try, kid");
                    return;
                }
            }
            catch
            {
                e.Channel.SendMessage("That's not a number");
                return;
            }
            string output = "";
            if (input % 3 == 0)
            {
                output += "fizz";
            }
            if (input % 5 == 0)
            {
                output += "buzz";
            }
            if (output == "")
            {
                e.Channel.SendMessage(input.ToString());
            }
            else
            {
                e.Channel.SendMessage(output);
            }
            return;
        }
    }
}