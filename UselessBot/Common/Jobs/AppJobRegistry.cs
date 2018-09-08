using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace UselessBot.Common.Jobs
{
    public class AppJobRegistry : Registry
    {
        private readonly IServiceProvider services;

        public AppJobRegistry(IServiceProvider services)
        {
            this.services = services;

            Schedule<HmmJob>().ToRunEvery(20).Minutes();
        }
    }
}
