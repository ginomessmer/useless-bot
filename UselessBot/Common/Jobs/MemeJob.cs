using Discord.WebSocket;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UselessBot.Data;
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
            Meme meme = await redditService.GetRandomTopMemeAsync();
            var channel = discordClient.GetGuild(Convert.ToUInt64(configuration["GuildId"]))
                .GetTextChannel(Convert.ToUInt64(configuration["Modules:Memes:ChannelId"]));

            await channel.SendMessageAsync(meme.ToString());
        }
    }
}
