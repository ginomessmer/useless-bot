using Discord.Commands;
using RedditSharp;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UselessBot.Core.Extensions;
using UselessBot.Core.Services;
using Microsoft.Extensions.Configuration;

namespace UselessBot.Modules
{
    public class RedditModule : ModuleBase
    {
        private readonly IRedditService redditService;
        private readonly IConfigurationRoot configuration;

        private static DateTime LastCommandAt;

        public RedditModule(IRedditService redditService, IConfigurationRoot configuration)
        {
            this.redditService = redditService;
            this.configuration = configuration;
        }

        [Command("nice"), Summary("things that make you say... nice")]
        public async Task GetRandomNiceSubmission()
        {
            var message = await Context.Channel.SendMessageAsync("Hold up a second...");
            var post = await redditService.GetRandomSubmissionAsync("nice");
            await message.ModifyAsync(m => m.Content = post.Url.ToString());

        }

        [Command("wtf"), Summary("What the")]
        public async Task GetRandomWtfSubmission()
        {
            var message = await Context.Channel.SendMessageAsync("Getting something you'll regret to see...");
            var post = await redditService.GetRandomSubmissionAsync("wtf");
            await message.ModifyAsync(m => m.Content = post.Url.ToString());

        }

        [Command("cat"), Summary("Obligatory")]
        public async Task GetRandomCatSubmission()
        {
            var message = await Context.Channel.SendMessageAsync(":cat:");
            var post = await redditService.GetRandomSubmissionAsync("cat");
            await message.ModifyAsync(m => m.Content = post.Url.ToString());

        }

        [Command("sad"), Summary("This is so sad")]
        public async Task GetRandomSadCatSubmission()
        {
            var message = await Context.Channel.SendMessageAsync(":sob:");
            var post = await redditService.GetRandomSubmissionAsync("sadcats");
            await message.ModifyAsync(m => m.Content = post.Url.ToString());

        }

        [Command("doggo"), Summary("Bork")]
        public async Task GetRandomDoggoSubmission()
        {
            var message = await Context.Channel.SendMessageAsync(":dog2:");
            var post = await redditService.GetRandomSubmissionAsync("doggos");
            await message.ModifyAsync(m => m.Content = post.Url.ToString());

        }

        [Command("hmm"), Summary("Hmm...")]
        public async Task GetRandomHmm()
        {
            if (LastCommandAt.AddSeconds(Convert.ToDouble(configuration["Modules:Hmm:CooldownPeriod"])) < DateTime.Now)
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
