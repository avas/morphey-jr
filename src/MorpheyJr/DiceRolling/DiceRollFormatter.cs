using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBotPlayground.DiceRolling
{
    public class DiceRollFormatter : IDiceRollFormatter
    {
        public string Format(SummaryRollResult rollResult)
        {
            var stringBuilder = new StringBuilder();

            var diceGroupIndex = 1;
            foreach (var groupResult in rollResult.DiceGroupRollResults)
            {
                var formattedGroupResult = FormatDiceGroupRoll(groupResult, diceGroupIndex++);
                stringBuilder.AppendLine(formattedGroupResult);
            }

            return stringBuilder.ToString();
        }

        private string FormatDiceGroupRoll(DiceGroupRollResult rollResult, int groupIndex)
        {
            string result;

            if (rollResult.DiceResults.Count > 1)
            {
                result = FormatMultiDiceGroupRoll(rollResult, groupIndex);
            }
            else
            {
                result = FormatSingleDiceGroupRoll(rollResult, groupIndex);
            }

            return result;
        }

        private string FormatMultiDiceGroupRoll(DiceGroupRollResult result, int groupIndex)
        {
            var stringBuilder = new StringBuilder();

            var groupContents = FormatDiceGroupContents(result);
            stringBuilder.AppendLine($"Группа #{groupIndex} ({groupContents}):");

            var diceIndex = 1;
            foreach (var diceResult in result.DiceResults)
            {
                var linePrefix = $"Кость #{diceIndex++}";
                var formattedDiceRoll = FormatSingleDiceRoll(diceResult, linePrefix);

                stringBuilder.AppendLine(formattedDiceRoll);
            }

            stringBuilder.AppendLine($"Сумма: {result.Sum} / {result.MaxSum}");

            return stringBuilder.ToString();
        }

        private string FormatSingleDiceGroupRoll(DiceGroupRollResult result, int groupIndex)
        {
            var firstDiceRollResult = result.DiceResults.First();
            var linePrefix = $"Одиночная кость #{groupIndex}";

            return FormatSingleDiceRoll(firstDiceRollResult, linePrefix);
        }

        private string FormatDiceGroupContents(DiceGroupRollResult groupRollResult)
        {
            var diceCountBySideCount = new Dictionary<int, int>();

            foreach (var rollResult in groupRollResult.DiceResults)
            {
                var currentSideCount = rollResult.MaxResult;

                if (diceCountBySideCount.TryGetValue(currentSideCount, out var diceCount))
                {
                    diceCountBySideCount[currentSideCount] = diceCount + 1;
                }
                else
                {
                    diceCountBySideCount.Add(currentSideCount, 1);
                }
            }

            var formattedDice = diceCountBySideCount.Select(x => x.Value == 1 ? $"d{x.Key}" : $"{x.Value}d{x.Key}");

            return string.Join("+", formattedDice);
        }

        private string FormatSingleDiceRoll(DiceRollResult result, string linePrefix)
        {
            return $"{linePrefix}: {result.Result} / {result.MaxResult}";
        }
    }
}