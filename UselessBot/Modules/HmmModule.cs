using Discord.Commands;
using RedditSharp;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UselessBot.Common.Extensions;

namespace UselessBot.Modules
{
    public class HmmModule : ModuleBase
    {
        private readonly Reddit reddit;

        public HmmModule(Reddit reddit)
        {
            this.reddit = reddit;
        }

        [Command("hmm")]
        public async Task GetRandomHmm()
        {
            var message = await Context.Channel.SendMessageAsync(":thinking:");
            var subreddit = await reddit.GetSubredditAsync("/r/hmmm");
            var hmm = (await subreddit.GetTop(10).ToList()).Random();
            await message.ModifyAsync(m => { m.Content = hmm.Url.ToString(); });
        }
    }
}
