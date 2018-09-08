using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UselessBot.Modules
{
    public class AdminModule : ModuleBase
    {
        private readonly DiscordSocketClient discordClient;

        public AdminModule(DiscordSocketClient discordClient)
        {
            this.discordClient = discordClient;
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

        [Command("bot 👋")]
        public async Task Shutdown()
        {
            await Context.Channel.SendMessageAsync("mkay thanks bye");
            Environment.Exit(-1);
        }
    }
}
