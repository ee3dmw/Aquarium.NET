using Aquarium.GiftShop.CastleWindsor;
using Aquarium.TriggerFish;
using Quartz;

namespace TriggerFishDemoService
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.GlobalContext.Properties["LogName"] = "TriggerFishDemo";
            log4net.Config.XmlConfigurator.Configure();

            Container.Install(new TriggerFishDemoServiceContainerInstaller());

            var jobData = new JobDataMap();
            jobData.Add("JobName", "Aquarium.TriggerFish");
            jobData.Add("JobDescription", "TriggerFish Demo - run a job on a customized schedule");
            TriggerFishSetupFactory.SetupService<FeedingTimeJob>(jobData, new FeedingTimeTriggers());
        }
    }
}
