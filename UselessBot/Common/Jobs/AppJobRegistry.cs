using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace UselessBot.Common.Jobs
{
    public class AppJobRegistry : Registry
    {
        // This registers all jobs and its schedule
        public AppJobRegistry()
        {
            Schedule<MemeJob>().ToRunNow().AndEvery(20).Minutes().Between(11, 0, 23, 59);
            Schedule<MemeJob>().ToRunEvery(10).Minutes().Between(0, 1, 2, 0);

            Schedule<HmmJob>().ToRunEvery(30).Minutes().Between(3, 0, 6, 0);
            Schedule<BotStatusJob>().ToRunNow().AndEvery(2).Minutes();
        }

        public static void JobStartedEventHandler(JobStartInfo arg)
        {
            Console.WriteLine($"[JOB] Starting job {arg.Name}...");
        }

        internal static void JobEndedEventHandler(JobEndInfo arg)
        {
            Console.WriteLine($"[JOB] Job {arg.Name} was completed (ran for {arg.Duration.TotalSeconds} seconds)");
        }

        internal static void JobFailedEventHandler(JobExceptionInfo arg)
        {
            Console.WriteLine($"[JOB] Job {arg.Name} failed: {arg.Exception.Message}", Color.Red);
        }
    }
}
