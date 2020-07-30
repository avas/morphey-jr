using System.Collections.Generic;

namespace DiscordBotPlayground.DiceRolling
{
    public interface IDiceRoller
    {
        SummaryRollResult Roll(IList<DiceGroup> diceGroups);
    }
}