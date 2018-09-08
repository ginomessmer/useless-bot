using Discord.WebSocket;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UselessBot.Services;

namespace UselessBot.Common.Jobs
{
    public class MemeJob : IJob
    {
        private readonly IRedditService redditService;
        private readonly DiscordSocketClient discordClient;
        private readonly IConfigurationRoot configuration;

        public MemeJob()
        {
            this.redditService = App.Services.GetService(typeof(IRedditService)) as IRedditService;
            this.discordClient = App.Services.GetService(typeof(DiscordSocketClient)) as DiscordSocketClient;
            this.configuration = App.Services.GetService(typeof(IConfigurationRoot)) as IConfigurationRoot;
        }

        public async void Execute()
        {
            Console.WriteLine("Executing Memes Job...");
            var url = await redditService.GetRandomMemeContentAsync();
            var channel = discordClient.GetGuild(Convert.ToUInt64(configuration["GuildId"]))
                .GetTextChannel(Convert.ToUInt64(configuration["Modules:Memes:ChannelId"]));

            await channel.SendMessageAsync($"Meme :clap: review :clap:\n{url}");
        }
    }
}
