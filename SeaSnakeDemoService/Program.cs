using System.Collections.Generic;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.Quartz;
using Aquarium.SeaSnake;
using Quartz;

namespace SeaSnakeDemoService
{
    class Program
    {
        static void Main(string[] args)
        {
            Container.Install(new SeaSnakeDemoContainerInstaller());
            var serviceDescription = "SeaSnake Demo Service.";
            var serviceName = "Aquarium.SeaSnake";
            var serviceDisplayName = "Aquarium.SeaSnake";

            var jobConfigs = CreateJobConfigs();

            SeaSnakeSetupFactory.SetupService(serviceDescription, serviceDisplayName, serviceName, jobConfigs);
        }

        private static List<JobConfig> CreateJobConfigs()
        {
            var feedingTimeTrigger = TriggerBuilder.Create()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            var cleaningTankTrigger = TriggerBuilder.Create()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            var jobConfigs = new List<JobConfig>();
            jobConfigs.Add(new JobConfig
            {
                JobDescription = "FeedingTimeMessage Job",
                JobTrigger = feedingTimeTrigger,
                JobType = typeof (FeedingTimeJob)
            });
            jobConfigs.Add(new JobConfig
            {
                JobDescription = "Clean Tank Job",
                JobTrigger = cleaningTankTrigger,
                JobType = typeof (CleanTankJob)
            });
            return jobConfigs;
        }
    }
}
