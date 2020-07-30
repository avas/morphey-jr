using System;
using System.Collections.Generic;

namespace DiscordBotPlayground.DiceRolling
{
    public class DiceRoller : IDiceRoller
    {
        public SummaryRollResult Roll(IList<DiceGroup> diceGroups)
        {
            var result = new SummaryRollResult();

            foreach (var group in diceGroups)
            {
                var groupRollResult = RollDiceGroup(group);
                result.DiceGroupRollResults.Add(groupRollResult);
            }

            return result;
        }

        public DiceGroupRollResult RollDiceGroup(DiceGroup diceGroup)
        {
            var result = new DiceGroupRollResult();

            foreach (var dice in diceGroup.Dice)
            {
                var rollResult = RollSingleDice(dice);

                result.MaxSum += rollResult.MaxResult;
                result.Sum += rollResult.Result;

                result.DiceResults.Add(rollResult);
            }

            return result;
        }

        private DiceRollResult RollSingleDice(Dice dice)
        {
            var random = new Random();
            var result = random.Next(1, dice.SideCount);

            return new DiceRollResult
            {
                MaxResult = dice.SideCount,
                Result = result
            };
        }
    }
}