using Discord.Commands;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Core.Data;
using UselessBot.Core.Services;

namespace UselessBot.Modules
{
    public class MemesModule : ModuleBase
    {
        private readonly IRedditService redditService;
        private readonly IFileStorageService storage;
        private readonly LiteDatabase liteDb;

        private static string previousMemeUrl;

        public MemesModule(IRedditService redditService, IFileStorageService storage, LiteDatabase liteDb)
        {
            this.redditService = redditService;
            this.storage = storage;
            this.liteDb = liteDb;
        }

        [Command("meme"), Summary("This sends a spicy dank meme for you")]
        [Alias("m")]
        public async Task RandomMeme()
        {
            var message = await Context.Channel.SendMessageAsync("Getting a spicy meme right now...");
            Meme meme = await redditService.GetRandomMemeAsync();

            previousMemeUrl = meme.ImageUrl;
            await message.ModifyAsync(m => m.Content = meme.ToString());
        }


        [Command("meme save"), Summary("Saves the previous meme to the bot storage")]
        [Alias("m s")]
        public async Task SavePreviousMeme()
        {
            var message = await Context.Channel.SendMessageAsync($"Saving latest meme to storage...");

            if(!String.IsNullOrEmpty(previousMemeUrl))
            {
                var client = new HttpClient();
                var stream = await client.GetStreamAsync(previousMemeUrl);
                var info = liteDb.FileStorage.Upload($"memes/{Guid.NewGuid().ToString()}", null, stream);

                previousMemeUrl = String.Empty;

                await message.ModifyAsync(m => m.Content = $"Done (#{info.Id}) :white_check_mark:");
            }
            else
            {
                await message.ModifyAsync(m => m.Content = $"You'll need to request a meme first before you attempt to save one");
            }
        }
    }
}
