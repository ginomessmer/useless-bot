using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Data;
using UselessBot.Services;

namespace UselessBot.Modules
{
    public class MemesModule : ModuleBase
    {
        private readonly IRedditService redditService;

        public MemesModule(IRedditService redditService)
        {
            this.redditService = redditService;
        }

        [Command("meme"), Summary("This sends a spicy dank meme for you")]
        [Alias("m")]
        public async Task RandomMeme()
        {
            var message = await Context.Channel.SendMessageAsync("Getting a spicy meme right now...");
            Meme meme = await redditService.GetRandomMemeAsync();
            await message.ModifyAsync(m => m.Content = meme.ToString());
        }
    }
}
