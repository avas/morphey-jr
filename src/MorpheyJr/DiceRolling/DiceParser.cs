using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DiscordBotPlayground.DiceRolling
{
    public class DiceParser : IDiceParser
    {
        private const string _diceSideCountSeparator = "d";

        public bool TryParse(string command, out IList<DiceGroup> result)
        {
            // Input string format: "2d6 d20 5d20+d6"

            if (string.IsNullOrWhiteSpace(command))
            {
                command = "d6";
            }

            result = new List<DiceGroup>();

            var diceGroups = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            foreach (var diceGroup in diceGroups)
            {
                if (TryParseDiceGroup(diceGroup, out var parsedGroup))
                {
                    result.Add(parsedGroup);
                }
            }

            return result.Any();
        }


        private bool TryParseDiceGroup(string diceGroup, out DiceGroup result)
        {
            var diceGroupEntries = diceGroup.Split("+", StringSplitOptions.RemoveEmptyEntries);

            result = new DiceGroup();

            foreach (var diceGroupEntry in diceGroupEntries)
            {
                if (TryParseDiceGroupEntry(diceGroupEntry, out var dice))
                {
                    foreach (var item in dice)
                    {
                        result.Dice.Add(item);
                    }
                }
            }

            return result.Dice.Any();
        }

        private bool TryParseDiceGroupEntry(string diceGroupEntry, out IList<Dice> dice)
        {
            dice = new List<Dice>();

            var diceParameters = diceGroupEntry.Split(_diceSideCountSeparator);
            var result = diceParameters.Length == 2;

            int count = 0;

            if (result)
            {
                var rawCount = diceParameters[0];
                if (string.IsNullOrWhiteSpace(rawCount))
                {
                    rawCount = "1";
                }

                result = int.TryParse(rawCount, NumberStyles.Integer, CultureInfo.InvariantCulture, out count);
            }

            int sideCount = 0;

            if (result)
            {
                var rawSideCount = diceParameters[1];
                result = int.TryParse(rawSideCount, NumberStyles.Integer, CultureInfo.InvariantCulture, out sideCount);
            }

            if (result)
            {
                for (int i = 0; i < count; i++)
                {
                    var item = new Dice
                    {
                        SideCount = sideCount
                    };

                    dice.Add(item);
                }
            }

            return result;
        }
    }
}