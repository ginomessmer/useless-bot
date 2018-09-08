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
using UselessBot.Services;
using Console = Colorful.Console;

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

            Console.WriteAscii("Useless Bot");

            await Task.Delay(-1);
        }

        private void InitializeServices(ServiceCollection serviceCollection)
        {
            Console.WriteLine("Initializing services...", Color.Blue);
            // Configuration builder
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            configuration = builder.Build();
            serviceCollection.AddSingleton(configuration);
            Console.WriteLine("Added configuration");


            // Database
            var botAppDbContext = new BotAppDbContext();
            serviceCollection.AddSingleton(botAppDbContext);
            botAppDbContext.Database.EnsureCreated();
            Console.WriteLine("Added database context");


            // Discord
            serviceCollection.AddSingleton(client);
            Console.WriteLine("Added Discord client");


            // Giphy
            Giphy giphy = new Giphy(configuration["GiphyToken"]);
            serviceCollection.AddSingleton(giphy);
            Console.WriteLine("Added Giphy client");


            // App services
            serviceCollection.AddSingleton<IQuotesService, QuotesService>();
            serviceCollection.AddSingleton<IGifService, GifService>();
            Console.WriteLine("Added app services");


            // Build the service provider
            this.services = serviceCollection.BuildServiceProvider();
            Console.WriteLine("Services were initialized", Color.Green);
        }

        private async Task InstallCommands()
        {
            Console.WriteLine("Adding commands...");
            client.MessageReceived += HandleMessageCommands;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
            Console.WriteLine("Commands were added", Color.Green);
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
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
                Console.WriteLine($":x: {result.ErrorReason}");
            }
        }
    }
}
