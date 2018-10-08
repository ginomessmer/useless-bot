using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UselessBot.WebApp.Services
{
    public class AuthService : IAuthService
    {
        public async Task<string> GetDiscordAccessTokenAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
