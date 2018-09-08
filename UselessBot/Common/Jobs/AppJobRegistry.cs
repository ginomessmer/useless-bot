using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace UselessBot.Common.Jobs
{
    public class AppJobRegistry : Registry
    {
        public AppJobRegistry()
        {
            Schedule<HmmJob>().ToRunNow().AndEvery(20).Minutes();
        }
    }
}
