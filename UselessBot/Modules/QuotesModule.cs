using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Data;
using UselessBot.Database;

namespace UselessBot.Modules
{
    public class QuotesModule : ModuleBase
    {
        private readonly BotAppDbContext _appDbContext;

        public QuotesModule(BotAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Command("q add"), Summary("Adds a new quote")]
        public async Task AddQuote(string key, [Remainder, Summary("Quote, obviously")] string quote)
        {
            await _appDbContext.Quotes.AddAsync(new Quote(quote, key, Context.Message.Author.Id, Context.Message.Author.Username));
            await _appDbContext.SaveChangesAsync();
            await Context.Channel.SendMessageAsync($":white_check_mark: Your quote has been added: `{key}`");
        }
    }
}
