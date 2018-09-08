using Discord.Commands;
using RedditSharp;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UselessBot.Common.Extensions;
using UselessBot.Services;
using Microsoft.Extensions.Configuration;

namespace UselessBot.Modules
{
    public class HmmModule : ModuleBase
    {
        private readonly IRedditService redditService;
        private readonly IConfigurationRoot configuration;

        private static DateTime LastCommandAt;

        public HmmModule(IRedditService redditService, IConfigurationRoot configuration)
        {
            this.redditService = redditService;
            this.configuration = configuration;
        }

        [Command("hmm"), Summary("Hmm...")]
        public async Task GetRandomHmm()
        {
            if(LastCommandAt.AddSeconds(Convert.ToDouble(configuration["Modules:Hmm:CooldownPeriod"])) < DateTime.Now)
            {
                var message = await Context.Channel.SendMessageAsync(":thinking:");
                
                var url = await redditService.GetLatestHmmContentAsync();
                await message.ModifyAsync(m => { m.Content = url; });
                LastCommandAt = DateTime.Now;
            }
            else
            {
                await Context.Channel.SendMessageAsync("Calm down :fire: You can't do another hmm just yet");
            }
        }
    }
}
