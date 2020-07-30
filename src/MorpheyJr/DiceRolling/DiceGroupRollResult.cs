using System.Collections.Generic;

namespace DiscordBotPlayground.DiceRolling
{
    public class DiceGroupRollResult
    {
        public IList<DiceRollResult> DiceResults { get; set; } = new List<DiceRollResult>();
        public int Sum { get; set; }
        public int MaxSum { get; set; }
    }
}