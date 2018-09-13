using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GiphyDotNet.Manager;
using GiphyDotNet.Model.Parameters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UselessBot.Modules
{
    public class AdminModule : ModuleBase
    {
        private readonly DiscordSocketClient discordClient;
        private readonly IConfigurationRoot configuration;
        private readonly Giphy giphyService;

        public AdminModule(DiscordSocketClient discordClient, IConfigurationRoot configuration, Giphy giphyService)
        {
            this.discordClient = discordClient;
            this.configuration = configuration;
            this.giphyService = giphyService;
        }

        [Command("status"), Summary("Displays the current status of the bot")]
        public async Task Status()
        {
            EmbedBuilder builder = new EmbedBuilder()
            {
                Title = "Useless Bot",
                Description = "An useless bot for all your useless daily entertainment needs.",
                Fields = new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = $"Started at",
                        Value = Process.GetCurrentProcess().StartTime.ToString()
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = $"Your User ID",
                        Value = Context.Message.Author.Id
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = $"Current Server ID",
                        Value = Context.Guild.Id,
                        IsInline = true
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = $"Channel ID",
                        Value = Context.Channel.Id,
                        IsInline = true
                    }
                },
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}"
                },
                Url = "https://uselessbot.pro",
                Color = Color.Blue
            };

            await Context.Channel.SendMessageAsync("", false, builder);
        }

        [Command("config reload"), Summary("Reloads the application config at runtime")]
        public async Task ChangeNickname()
        {
            if (Context.Message.Author.Id == configuration.GetSection("OwnerId").Get<ulong>())
            {
                configuration.Reload();
                await Context.Channel.SendMessageAsync("Configuration was reloaded :white_check_mark:");
            }
        }

        [Command("die"), Summary("Like this bot to instantly die")]
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

        [Command("ban"), Summary("It works 100%, no virus")]
        public async Task Ban([Remainder]SocketGuildUser user)
        {
            var gif = await giphyService.RandomGif(new RandomParameter()
            {
                Tag = "banhammer"
            });

            var message = $"{user.Mention} :wave: \n{gif.Data.Url}";
            await Context.Channel.SendMessageAsync(message);
        }
    }
}
