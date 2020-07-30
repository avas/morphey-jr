using System.Collections.Generic;

namespace DiscordBotPlayground.DiceRolling
{
    public interface IDiceParser
    {
        bool TryParse(string command, out IList<DiceGroup> result);
    }
}