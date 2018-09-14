using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Core.Data;

namespace UselessBot.Core.Services
{
    public interface IGifService
    {
        Task<Gif> AddGifAsync(string url, string key, IUser user);
        Task<Gif> GetRandomGifAsync();
        Task<Gif> FindGifAsync(string key);
        Task RemoveGifAsync(string key);
    }
}
