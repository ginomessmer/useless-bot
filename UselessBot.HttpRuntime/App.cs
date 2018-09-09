using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace UselessBot.HttpRuntime
{
    public class App
    {
        private readonly IServiceProvider services;
        public string[] Args { get; }

        public App(string[] args, IServiceProvider services)
        {
            this.services = services;
            this.Args = args;
        }

        public async void Start()
        {
            await CreateWebHostBuilder(this.Args).Build().RunAsync();
        }

        private IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}
