using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UselessBot.WebApp.Services
{
    public interface IAuthService
    {
        Task<string> GetDiscordAccessTokenAsync(HttpContext context);
    }
}
