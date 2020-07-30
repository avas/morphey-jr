using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotPlayground.Configuration;
using DiscordBotPlayground.DiceRolling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DiscordBotPlayground
{
    public class Program
    {
        private DiscordSocketClient _discordClient;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            AppDomain.CurrentDomain.ProcessExit += HandleProcessExit;
            await SetUpDependencyInjectionAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private void HandleProcessExit(object sender, EventArgs e)
        {
            var task = _discordClient.StopAsync();
        }


        private async Task SetUpDependencyInjectionAsync()
        {
            var configuration = LoadConfiguration();
            var discordConnectionSettings = configuration.GetSection("DiscordConnectionSettings").Get<DiscordConnectionSettings>();

            _discordClient = new DiscordSocketClient();
            _discordClient.Log += Log;

            await _discordClient.LoginAsync(discordConnectionSettings.TokenType, discordConnectionSettings.Token);
            await _discordClient.StartAsync();

            var serviceCollection = new ServiceCollection()
                .AddLogging()
                .AddSingleton(_discordClient)
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<IDiceParser, DiceParser>()
                .AddSingleton<IDiceRoller, DiceRoller>()
                .AddSingleton<IDiceRollFormatter, DiceRollFormatter>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var commandHandler = serviceProvider.GetService<CommandHandler>();
            await commandHandler.InstallCommandsAsync();
        }

        private IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
