using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Data;
using UselessBot.Database;

namespace UselessBot.Services
{
    public class GifService : IGifService
    {
        private readonly BotAppDbContext appDbContext;

        public GifService(BotAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Gif> AddGifAsync(string url, string key, IUser user)
        {
            var gif = await appDbContext.Gifs.AddAsync(new Gif(url, key, user.Id, user.Username));
            await appDbContext.SaveChangesAsync();

            return gif.Entity;
        }
    }
}
