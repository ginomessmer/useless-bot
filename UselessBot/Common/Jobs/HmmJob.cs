using Discord.WebSocket;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UselessBot.Core.Services;
using Console = Colorful.Console;

namespace UselessBot.Common.Jobs
{
    public class HmmJob : IJob
    {
        private readonly IRedditService redditService;
        private readonly DiscordSocketClient discordClient;
        private readonly IConfigurationRoot configuration;

        public HmmJob()
        {
            this.redditService = App.Services.GetService(typeof(IRedditService)) as IRedditService;
            this.discordClient = App.Services.GetService(typeof(DiscordSocketClient)) as DiscordSocketClient;
            this.configuration = App.Services.GetService(typeof(IConfigurationRoot)) as IConfigurationRoot;
        }

        public async void Execute()
        {
            var hmm = await redditService.GetLatestHmmContentAsync();
            var channel = discordClient.GetGuild(Convert.ToUInt64(configuration["GuildId"]))
                .GetTextChannel(Convert.ToUInt64(configuration["Modules:Hmm:ChannelId"]));

            await channel.SendMessageAsync($":thinking: {hmm}");
        }
    }
}
