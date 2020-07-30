using System.Collections.Generic;

namespace DiscordBotPlayground.DiceRolling
{
    public class DiceGroup
    {
        public IList<Dice> Dice { get; set; } = new List<Dice>();
    }
}