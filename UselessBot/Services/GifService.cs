using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UselessBot.Common.Extensions;
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

        public async Task<Gif> FindGifAsync(string key)
        {
            return await Task<Gif>.Run(() =>
            {
                var gif = appDbContext.Gifs.Where(g => g.Key == key).Random();
                return gif;
            });
        }

        public async Task<Gif> GetRandomGifAsync()
        {
            return await Task<Gif>.Run(() =>
            {
                var gif = appDbContext.Gifs.Random();
                return gif;
            });
        }

        public async Task RemoveGifAsync(string key)
        {
            var gif = appDbContext.Gifs.FirstOrDefault(g => g.Key == key);

            if(gif != null)
            {
                appDbContext.Gifs.Remove(gif);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
