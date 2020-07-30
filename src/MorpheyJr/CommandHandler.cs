using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBotPlayground
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _serviceProvider;

        public CommandHandler(DiscordSocketClient discordClient, CommandService commandService, IServiceProvider serviceProvider)
        {
            _discordClient = discordClient;
            _commandService = commandService;
            _serviceProvider = serviceProvider;
        }

        public async Task InstallCommandsAsync()
        {
            _discordClient.MessageReceived += HandleCommandAsync;

            await _commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _serviceProvider);
        }


        private async Task HandleCommandAsync(SocketMessage message)
        {
            var userMessage = message as SocketUserMessage;
            if (userMessage == null)
            {
                return;
            }

            int argPos = 0;
            if (!userMessage.HasCharPrefix('!', ref argPos) || message.Author.IsBot)
            {
                return;
            }

            var context = new SocketCommandContext(_discordClient, userMessage);
            await _commandService.ExecuteAsync(context, argPos, _serviceProvider);
        }
    }
}