using System;
using System.Threading;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.RabbitMQ;
using Aquarium.Octopus;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ;

namespace OctopusDemoService
{
    class Program
    {
        static void Main(string[] args)
        {
            Container.Install(new OctopusContainerInstaller());
            var serviceDescription = "Octopus Demo Service.";
            var serviceName = "Aquarium.Octopus";
            var serviceDisplayName = "Aquarium.Octopus";

            OctopusSetupFactory.SetupService(serviceDescription, serviceDisplayName, serviceName);
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

    public class FeedingTimeMessageConsumer : MessageConsumer<FeedingTime>
    {
        public override void Consume(FeedingTime message)
        {
            //Thread.Sleep(20000);
            Console.WriteLine("FeedingTime! Tank {0} on Thread {1} at {2}", message.Id, Thread.CurrentThread.ManagedThreadId, DateTime.Now);
        }
    }

    public class CleanTankTimeMessageConsumer : MessageConsumer<CleanTankTime>
    {
        public override void Consume(CleanTankTime message)
        {
            //Thread.Sleep(20000);
            Console.WriteLine("Cleaning Tanks Time! Tank {0} on Thread {1} at {2}", message.Id, Thread.CurrentThread.ManagedThreadId, DateTime.Now);
        }
    }

    public class OctopusContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<FeedingTimeMessageConsumer>().LifestyleTransient());
            container.Register(Component.For<CleanTankTimeMessageConsumer>().LifestyleTransient());
        }
    }
}
