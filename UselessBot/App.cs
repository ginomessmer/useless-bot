using Discord;
using Discord.Commands;
using Discord.WebSocket;
using FluentScheduler;
using GiphyDotNet.Manager;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Common.Jobs;
using UselessBot.Core.Database;
using UselessBot.Core.Services;
using Console = Colorful.Console;

namespace UselessBot
{
    public class App
    {
        private DiscordSocketClient discordClient;
        public static CommandService CommandService;

        private IConfigurationRoot configuration;

        private ServiceCollection serviceCollection;
        public static IServiceProvider Services;


        public async Task RunAsync()
        {
            discordClient = new DiscordSocketClient();
            CommandService = new CommandService();

            serviceCollection = new ServiceCollection();
            this.InitializeServices(serviceCollection);

            await this.InstallCommands();

            await discordClient.LoginAsync(TokenType.Bot, configuration["DiscordBotToken"]);
            await discordClient.StartAsync();

            await this.InitializeJobs();

            Console.WriteAscii("Useless Bot");

            await Task.Delay(-1);
        }

        private Task InitializeJobs()
        {
            JobManager.Initialize(new AppJobRegistry());

            JobManager.JobStart += AppJobRegistry.JobStartedEventHandler;
            JobManager.JobEnd += AppJobRegistry.JobEndedEventHandler;
            JobManager.JobException += AppJobRegistry.JobFailedEventHandler;

            return Task.CompletedTask;
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


            // Storage
            var storageDb = new LiteDatabase("app.storage.db");
            serviceCollection.AddSingleton(storageDb);
            Console.WriteLine("Added file storage");


            // Discord
            serviceCollection.AddSingleton(discordClient);
            Console.WriteLine("Added Discord client");


            // Reddit
            var redditWebAgent = new BotWebAgent(configuration["Reddit:Username"], configuration["Reddit:Password"],
                configuration["Reddit:ClientId"], configuration["Reddit:ClientSecret"], configuration["Reddit:RedirectUri"]);
            Reddit redditClient = new Reddit(redditWebAgent);
            serviceCollection.AddSingleton(redditClient);
            Console.WriteLine("Added Reddit client");


            // Giphy
            Giphy giphy = new Giphy(configuration["GiphyToken"]);
            serviceCollection.AddSingleton(giphy);
            Console.WriteLine("Added Giphy client");


            // YouTube
            //YouTubeService youTubeService = new YouTubeService(new BaseClientService.Initializer
            //{
            //    ApplicationName = "Useless Bot",
            //    ApiKey = configuration["YouTubeApiKey"]
            //});
            //serviceCollection.AddSingleton(youTubeService);


            // App services
            serviceCollection.AddSingleton<IFileStorageService, FileStorageService>();
            serviceCollection.AddSingleton<IQuotesService, QuotesService>();
            serviceCollection.AddSingleton<IGifService, GifService>();
            serviceCollection.AddSingleton<IRedditService, RedditService>();
            Console.WriteLine("Added app services");


            // Build the service provider
            Services = serviceCollection.BuildServiceProvider();
            Console.WriteLine("Services were initialized", Color.Green);
        }

        private async Task InstallCommands()
        {
            Console.WriteLine("Adding commands...");
            discordClient.MessageReceived += HandleMessageCommands;
            await CommandService.AddModulesAsync(Assembly.GetEntryAssembly());
            Console.WriteLine("Commands were successfully added", Color.Green);
        }

        public async Task HandleMessageCommands(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if(!(message.HasCharPrefix(configuration["Prefix"].ToCharArray()[0], ref argPos)) || message.HasMentionPrefix(discordClient.CurrentUser, ref argPos)) return;

            var context = new CommandContext(discordClient, message);
            var result = await CommandService.ExecuteAsync(context, argPos, Services);

            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
                Console.WriteLine($":x: {result.ErrorReason}");
            }
        }
    }
}
