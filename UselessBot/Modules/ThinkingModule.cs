using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Core.Services;

namespace UselessBot.Modules
{
    public class ThinkingModule : ModuleBase
    {
        private readonly IRedditService redditService;

        public ThinkingModule(IRedditService redditService)
        {
            this.redditService = redditService;
        }

        [Command("thinking"), Summary(":thinking:")]
        [Alias("t", ":thinking:", "🤔")]
        public async Task Thinking()
        {
            var post = await redditService.GetRandomSubmissionAsync("thinking");
            await Context.Channel.SendMessageAsync($":thinking: {post.Url.ToString()}");
        }
    }
}
