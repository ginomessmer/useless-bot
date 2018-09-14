using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Core.Data;
using UselessBot.Core.Database;
using UselessBot.Core.Services;

namespace UselessBot.Modules
{
    public class QuotesModule : ModuleBase
    {
        private readonly IQuotesService quotesService;

        public QuotesModule(IQuotesService quotesService)
        {
            this.quotesService = quotesService;
        }

        [Command("q add"), Summary("Adds a new quote")]
        public async Task AddQuote(string key, [Remainder, Summary("Quote, obviously")] string quote)
        {
            await quotesService.AddQuoteAsync(key, quote, Context.User);
            await Context.Channel.SendMessageAsync($":white_check_mark: Your quote has been added: `{key}`");
        }

        [Command("q"), Summary("Gets a random quote")]
        public async Task GetRandomQuote()
        {
            var quote = await quotesService.GetRandomQuoteAsync();
            await Context.Channel.SendMessageAsync(quote.ToMessageString());
        }

        [Command("q rem"), Summary("Removes a quote by its given key")]
        public async Task RemoveQuote(string key)
        {
            await quotesService.RemoveQuoteAsync(key);
            await Context.Channel.SendMessageAsync($" Quote `{key}` is gone, forever");
        }
    }
}
