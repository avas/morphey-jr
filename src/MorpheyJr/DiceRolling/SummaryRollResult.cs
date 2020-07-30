using System.Collections.Generic;

namespace DiscordBotPlayground.DiceRolling
{
    public class SummaryRollResult
    {
        public IList<DiceGroupRollResult> DiceGroupRollResults { get; set; } = new List<DiceGroupRollResult>();
    }
}