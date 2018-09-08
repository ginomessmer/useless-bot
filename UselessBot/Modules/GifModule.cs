using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Services;

namespace UselessBot.Modules
{
    public class GifModule : ModuleBase
    {
        private readonly IGifService service;

        public GifModule(IGifService service)
        {
            this.service = service;
        }

        [Command("gif add")]
        public async Task AddGif(string url, [Remainder] string key)
        {
            await service.AddGifAsync(url, key, Context.Message.Author);
            await Context.Channel.SendMessageAsync("Done :white_check_mark:");
        }
    }
}
