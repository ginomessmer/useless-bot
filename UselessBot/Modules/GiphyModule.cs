using Discord.Commands;
using GiphyDotNet.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UselessBot.Modules
{
    public class GiphyModule : ModuleBase
    {
        private readonly Giphy _giphyService;

        public GiphyModule(Giphy giphyService)
        {
            _giphyService = giphyService;
        }

        [Command("rgif"), Summary("Sends a random GIF")]
        public async Task RandomGif([Remainder, Summary("Search term")] string term)
        {
            var result = await _giphyService.RandomGif(new GiphyDotNet.Model.Parameters.RandomParameter()
            {
                Tag = term
            });

            await Context.Channel.SendMessageAsync(result.Data.Url);
        }
    }
}
