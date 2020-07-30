using Discord.Commands;
using DiscordBotPlayground.DiceRolling;
using System.Threading.Tasks;

namespace DiscordBotPlayground.CommandModules
{
    public class RollDiceModule : ModuleBase<SocketCommandContext>
    {
        private readonly IDiceParser _diceParser;
        private readonly IDiceRoller _diceRoller;
        private readonly IDiceRollFormatter _diceRollFormatter;

        public RollDiceModule(IDiceParser diceParser, IDiceRoller diceRoller, IDiceRollFormatter diceRollFormatter)
        {
            _diceParser = diceParser;
            _diceRoller = diceRoller;
            _diceRollFormatter = diceRollFormatter;
        }

        [Command("roll")]
        public async Task RollDice([Remainder] string diceMessage)
        {
            if (!_diceParser.TryParse(diceMessage, out var diceGroups))
            {
                await ReplyAsync("DOES NOT COMPUTE");
                return;
            }

            var rollResult = _diceRoller.Roll(diceGroups);
            var formattedMessage = _diceRollFormatter.Format(rollResult);

            await ReplyAsync(formattedMessage);
        }
    }
}