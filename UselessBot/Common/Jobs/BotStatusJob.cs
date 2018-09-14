using Discord.WebSocket;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UselessBot.Core.Extensions;
using UselessBot.Core.Services;

namespace UselessBot.Common.Jobs
{
    public class BotStatusJob : IJob
    {
        private readonly DiscordSocketClient discordClient;
        private readonly IConfigurationRoot configuration;

        public BotStatusJob()
        {
            this.discordClient = App.Services.GetService(typeof(DiscordSocketClient)) as DiscordSocketClient;
            this.configuration = App.Services.GetService(typeof(IConfigurationRoot)) as IConfigurationRoot;
        }

        public void Execute()
        {
            var collection = configuration.GetSection("Resources").GetSection("GameStatusCollection").Get<IList<string>>();
            string randomString = collection.Random();
            discordClient.SetGameAsync(randomString);
        }
    }
}
