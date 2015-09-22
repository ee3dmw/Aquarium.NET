using System.Collections.Generic;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.RabbitMQ;
using Aquarium.SeaSnake;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ;
using Quartz;

namespace SeaSnakeDemoService
{
    class Program
    {
        static void Main(string[] args)
        {
            Container.Install(new SeaSnakeContainerInstaller());
            var serviceDescription = "SeaSnake Demo Service.";
            var serviceName = "Aquarium.SeaSnake";
            var serviceDisplayName = "Aquarium.SeaSnake";

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger7", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            var trigger2 = TriggerBuilder.Create()
                .WithIdentity("trigger5", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            List<JobConfig> jobConfigs = new List<JobConfig>();
            jobConfigs.Add(new JobConfig { JobDescription = "FeedingTime Job", JobTrigger = trigger, JobType = typeof(FeedingTimeJob) });
            jobConfigs.Add(new JobConfig { JobDescription = "Clean Tank Job", JobTrigger = trigger2, JobType = typeof(CleanTankJob) });

            SeaSnakeSetupFactory.SetupService(serviceDescription, serviceDisplayName, serviceName, jobConfigs);
        }
    }

    [Queue("FeedingTime", ExchangeName = Constants.DefaultRabbitMqExchangeName)]
    public class FeedingTime : Message
    {
        public int Id { get; set; }
    }

    [Queue("CleanTankTime", ExchangeName = Constants.DefaultRabbitMqExchangeName)]
    public class CleanTankTime : Message
    {
        public int Id { get; set; }
    }

    public class FeedingTimeJob : IJob
    {
        private readonly IBus _bus;

        public FeedingTimeJob(IBus bus)
        {
            _bus = bus;
        }

        public void Execute(IJobExecutionContext context)
        {
            _bus.Publish(new FeedingTime() { Id = 1 }, "FeedingTime");
        }
    }

    public class CleanTankJob : IJob
    {
        private readonly IBus _bus;

        public CleanTankJob(IBus bus)
        {
            _bus = bus;
        }

        public void Execute(IJobExecutionContext context)
        {
            _bus.Publish(new CleanTankTime(), "CleanTankTime");
        }
    }

    public class SeaSnakeContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<FeedingTimeJob>().LifestyleTransient());
            container.Register(Component.For<CleanTankJob>().LifestyleTransient());
        }
    }

    
}
