using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UselessBot.Core.Data;
using UselessBot.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UselessBot.WebApp.Controllers
{
    [Route("api/quotes")]
    [Authorize()]
    public class QuotesController : Controller
    {
        private readonly IQuotesService quotesService;

        public QuotesController(IQuotesService quotesService)
        {
            this.quotesService = quotesService;
        }

        [HttpGet]
        public async Task<IEnumerable<Quote>> GetQuotes()
        {
            return await quotesService.GetAllQuotesAsync();
        }

        [HttpGet("random")]
        public async Task<Quote> GetRandomQuote()
        {
            return await quotesService.GetRandomQuoteAsync();
        }
    }
}
