using System;
using System.Linq;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.EasyNetQ;
using Aquarium.GiftShop.RabbitMQ;
using Aquarium.Octopus.Config;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ;
using EasyNetQ.Loggers;

namespace Aquarium.Octopus
{
    public class OctupusContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMapRabbitMqConsumers>().ImplementedBy<ConfigConsumersMapping>().LifestyleSingleton());
            container.Register(Component.For<OctopusService>().LifestyleTransient());
            container.Register(Component.For<ClassNameOnlyTypeSerializer>().LifestyleSingleton());
            container.Register(Component.For<IBus>().UsingFactoryMethod(CreateBusBuilder()).LifestyleSingleton());
        }

        private static Func<IBus> CreateBusBuilder()
        {
            return () => BusBuilder.CreateMessageBus(x =>
                x.Register<IEasyNetQLogger>(_ => new ConsoleLogger())
                    .Register<ITypeNameSerializer>(_ => Container.Resolve<ClassNameOnlyTypeSerializer>()));
        }
    }
}