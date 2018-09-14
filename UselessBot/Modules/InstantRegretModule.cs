using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UselessBot.Core.Extensions;

namespace UselessBot.Modules
{
    public class InstantRegretModule : ModuleBase
    {
        //private readonly YouTubeService youTubeService;

        //public InstantRegretModule(YouTubeService youTubeService)
        //{
        //    this.youTubeService = youTubeService;
        //}

        [Command("instantregret")]
        [Alias("ir")]
        public async Task GetInstantRegretVideo()
        {
            await Context.Channel.SendMessageAsync("This hasn\'t been implemented yet, I'm sorry.");            
        }
    }
}
