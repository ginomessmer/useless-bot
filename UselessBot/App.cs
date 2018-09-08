using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GiphyDotNet.Manager;
using GiphyDotNet.Model.Parameters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Database;

namespace UselessBot
{
    public class App
    {
        private DiscordSocketClient client;
        private CommandService commands;

        private ServiceCollection serviceCollection;
        private IServiceProvider services;
        private IConfigurationRoot configuration;

        public async Task RunAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            serviceCollection = new ServiceCollection();
            this.InitializeServices(serviceCollection);

            await this.InstallCommands();

            await client.LoginAsync(TokenType.Bot, configuration["DiscordBotToken"]);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private void InitializeServices(ServiceCollection serviceCollection)
        {
            // Configuration builder
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            configuration = builder.Build();
            serviceCollection.AddSingleton(configuration);


            // Database
            var botAppDbContext = new BotAppDbContext();
            serviceCollection.AddSingleton(botAppDbContext);
            botAppDbContext.Database.EnsureCreated();


            // Discord
            serviceCollection.AddSingleton(client);


            // Giphy
            Giphy giphy = new Giphy(configuration["GiphyToken"]);
            serviceCollection.AddSingleton(giphy);


            // Build the service provider
            this.services = serviceCollection.BuildServiceProvider();
        }

        private async Task InstallCommands()
        {
            client.MessageReceived += HandleMessageCommands;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleMessageCommands(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if(!(message.HasCharPrefix(configuration["Prefix"].ToCharArray()[0], ref argPos)) || message.HasMentionPrefix(client.CurrentUser, ref argPos)) return;

            var context = new CommandContext(client, message);
            var result = await commands.ExecuteAsync(context, argPos, services);

            if (!result.IsSuccess)
                Console.WriteLine(result.ErrorReason);
        }
    }
}
