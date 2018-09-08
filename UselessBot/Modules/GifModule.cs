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

        [Command("gif add"), Summary("Adds a new GIF to the bot database")]
        public async Task AddGif(string url, [Remainder] string key)
        {
            await service.AddGifAsync(url, key, Context.Message.Author);
            await Context.Channel.SendMessageAsync("Done :white_check_mark:");
        }

        [Command("gif rem"), Summary("Removes a GIF by its key, what else did you expect")]
        public async Task RemoveGif([Remainder] string key)
        {
            await service.RemoveGifAsync(key);
            await Context.Channel.SendMessageAsync("Consider it gone :put_litter_in_its_place:");
        }

        [Command("gif r"), Summary("Sends a random GIF straight from the database")]
        public async Task RandomGif()
        {
            var gif = await service.GetRandomGifAsync();
            await Context.Channel.SendMessageAsync(gif.Content);
        }
    }
}
