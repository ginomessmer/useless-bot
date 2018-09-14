using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UselessBot.Core.Extensions;
using UselessBot.Core.Data;
using UselessBot.Core.Database;

namespace UselessBot.Core.Services
{
    public class QuotesService : IQuotesService
    {
        private readonly BotAppDbContext _appDbContext;

        public QuotesService(BotAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Quote> AddQuoteAsync(string key, string content, IUser user)
        {
            var value = _appDbContext.Quotes.Add(new Quote(content, key, user.Id, user.Username));
            await _appDbContext.SaveChangesAsync();

            return value.Entity;
        }

        public async Task<Quote> GetRandomQuoteAsync()
        {
            return await Task.Run<Quote>(() =>
            {
                return _appDbContext.Quotes.Random();
            });
        }

        public async Task RemoveQuoteAsync(string key)
        {
            var quote = _appDbContext.Quotes.FirstOrDefault(q => q.Key == key);
            if(quote != null)
            {
                _appDbContext.Remove(quote);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
