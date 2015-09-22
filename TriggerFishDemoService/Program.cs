using System;
using System.Collections.Generic;
using System.Threading;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.TriggerFish;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Quartz;

namespace TriggerFishDemoService
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.GlobalContext.Properties["LogName"] = "TriggerFishDemo";
            log4net.Config.XmlConfigurator.Configure();

            Container.Install(new TriggerFishDempServiceContainerInstaller());

            var jobData = new JobDataMap();
            jobData.Add("JobName", "Aquarium.TriggerFish");
            jobData.Add("JobDescription", "TriggerFish Demo Service");
            TriggerFishSetupFactory.SetupService<FeedingTimeJob>(jobData, new FeedingTimeTriggers());
        }
    }

    public class TriggerFishDempServiceContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<FeedingTimeJob>().LifestyleTransient());
        }
    }

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

    public class FeedingTimeTriggers : IHaveLotsOfTriggers
    {
        public List<ITrigger> GetTriggers()
        {
            var triggers = new List<ITrigger>();
            triggers.Add(TriggerBuilder.Create().WithSimpleSchedule(s => s.WithIntervalInSeconds(5).RepeatForever().Build()).UsingJobData("Food", "Fish Eggs").Build());
            triggers.Add(TriggerBuilder.Create().WithSimpleSchedule(s => s.WithIntervalInSeconds(20).RepeatForever().Build()).UsingJobData("Food", "Eels").Build());
            return triggers;
        }
    }
}
