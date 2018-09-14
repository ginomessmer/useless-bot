using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Core.Data;

namespace UselessBot.Core.Services
{
    public interface IQuotesService
    {
        Task<Quote> GetRandomQuoteAsync();
        Task<Quote> AddQuoteAsync(string key, string content, IUser user);

        Task RemoveQuoteAsync(string key);
    }
}
