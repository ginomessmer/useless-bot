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

        [Command("#1")]
        public async Task NumberOne()
        {
            await Context.Channel.SendMessageAsync("https://www.youtube.com/watch?v=PfYnvDL0Qcw");
        }

        [Command("anime")]
        public async Task NoAnime()
        {
            await Context.Channel.SendMessageAsync("https://i.imgur.com/UeiCRgj.png");
        }

        [Command("help")]
        public async Task Help()
        {
            await Context.Channel.SendMessageAsync("https://i.imgur.com/y8Ea8jB.gif");
        }

        [Command("alexa")]
        public async Task Alexa()
        {
            await Context.Channel.SendMessageAsync("This is so sad, alexa play despacito\n https://www.youtube.com/watch?v=kJQP7kiw5Fk");
        }
    }
}
