using System;
using System.Threading;
using Quartz;

namespace TriggerFishDemoService
{
    public class FeedingTimeJob : IInterruptableJob
    {
        static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FeedingTimeJob));

        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.MergedJobDataMap;
            string food = dataMap.GetString("Food");
            Log.InfoFormat("Feeding Time, {0} at {1} on thread {2}", food, DateTime.Now, Thread.CurrentThread.ManagedThreadId);
        }

        public void Interrupt()
        {
            Log.Info("Kill All Trigger Fish!!!");
        }
    }
}