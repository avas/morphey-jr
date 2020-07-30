using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBotPlayground.CommandModules
{
    public class BugogaModule : ModuleBase<SocketCommandContext>
    {
        [Command("bugoga")]
        [Alias("бугога")]
        public Task SendBugoga()
        {
            return ReplyAsync("Бугога");
        }
    }
}