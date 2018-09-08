using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UselessBot.Modules
{
    public class ActionsModule : ModuleBase
    {
        [Command("doubt")]
        public async Task Doubt()
        {
            await Context.Channel.SendMessageAsync("https://i.imgur.com/o0E7hLg.png");
        }
    }
}
