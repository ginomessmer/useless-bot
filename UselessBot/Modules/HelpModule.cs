using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace UselessBot.Modules
{
    public class HelpModule : ModuleBase
    {
        [Command("help export"), Summary("Exports all commands and sends them to the current channel")]
        public async Task ExportCommandsToJson()
        {
            var list = new List<object>();

            foreach(var module in App.CommandService.Modules)
            {
                foreach(var command in module.Commands)
                {
                    list.Add(new
                    {
                        Command = command.Aliases,
                        Parameters = command.Parameters.Select(p => new { p.Name, p.Summary, p.DefaultValue, p.IsOptional }),
                        Summary = command.Summary,
                        Module = command.Module.Name
                    });
                }
            }

            string commands = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText("_commands.json", commands);

            await Context.Channel.SendFileAsync("_commands.json");
        }
    }
}
