using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UselessBot.Modules
{
    public class AdminModule : ModuleBase
    {
        private readonly DiscordSocketClient discordClient;
        private readonly IConfigurationRoot configuration;

        public AdminModule(DiscordSocketClient discordClient, IConfigurationRoot configuration)
        {
            this.discordClient = discordClient;
            this.configuration = configuration;
        }

        [Command("bot nick")]
        public async Task ChangeNickname([Remainder] string nickname)
        {
            var user = await Context.Guild.GetCurrentUserAsync(CacheMode.AllowDownload);
            await user.ModifyAsync(u =>
            {
                u.Nickname = nickname;
            });

            await Context.Channel.SendMessageAsync("Done :white_check_mark:");
        }

        [Command("die")]
        public async Task Shutdown()
        {
            if(Context.Message.Author.Id == configuration.GetSection("OwnerId").Get<ulong>())
            {
                await Context.Channel.SendMessageAsync("Bye, have a great time :wave: \n_Shutting down..._");
                Environment.Exit(-1);
            }
            else
            {
                await Context.Channel.SendMessageAsync("no u :neutral_face:");
            }
        }
    }
}
