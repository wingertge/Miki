using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;

namespace Miki.Core.Commands
{
    class Roll : Command
    {
        Random r = new Random();

        public override void Initialize()
        {
            id = "roll";
            appearInHelp = true;
            description = "rolls a dice, add a number to roll a custom dice";

            parameterType = ParameterType.BOTH;
            usage = new string[] { "number" };

            expandedDescription = "rolls a dice, use tags like 1d6 or 4 to determine it's type of rolls";
            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            if (message.Split(' ').Length == 1)
            {
                e.Channel.SendMessage(RollDice(100).ToString());
                return;
            }
            string number = message.Split(' ')[1];

            if (number.Split('d').Length > 1)
            {
                string[] rpgDice = number.Split('d');
                int amountOfDice = int.Parse(rpgDice[0]);
                int maxRoll = int.Parse(rpgDice[1]);
                string outputCalc = "";
                int totalRoll = 0;

                int[] rolls = new int[amountOfDice];

                for (int i = 0; i < amountOfDice; i++)
                {
                    if (outputCalc != "")
                    {
                        outputCalc += "+";
                    }
                    rolls[i] = RollDice(maxRoll);
                    totalRoll += rolls[i];
                    outputCalc += rolls[i].ToString();
                }

                e.Channel.SendMessage(totalRoll + " (" + outputCalc + ")");
                return;
            }
            e.Channel.SendMessage(RollDice(int.Parse(number)).ToString());
            return;
        }

        int RollDice(int max)
        {
            return r.Next(0, max) + 1;
        }
    }
}
