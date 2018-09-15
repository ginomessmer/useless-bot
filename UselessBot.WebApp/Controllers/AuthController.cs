using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UselessBot.WebApp.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet("discord")]
        public IActionResult DiscordLogin(string returnUrl = "/auth/discord/details")
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [HttpGet("discord/details")]
        public IActionResult DiscordDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new JsonResult(new
                {
                    Claims = new
                    {
                        Id = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                        Username = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value,
                        Email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                    }
                });
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}