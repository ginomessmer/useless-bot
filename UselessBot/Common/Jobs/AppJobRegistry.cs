using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace UselessBot.Common.Jobs
{
    public class AppJobRegistry : Registry
    {
        // This registers all jobs and its schedule
        public AppJobRegistry()
        {
            Schedule<MemeJob>().ToRunNow().AndEvery(10).Minutes();
            Schedule<HmmJob>().ToRunEvery(1).Hours();
            Schedule<BotStatusJob>().ToRunNow().AndEvery(2).Minutes();
        }
    }
}
