using System;
using System.Linq;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.EasyNetQ;
using Aquarium.GiftShop.RabbitMQ;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ;
using EasyNetQ.Loggers;

namespace Aquarium.SeaSnake
{
    public class SeaSnakeContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMapRabbitMqConsumers>().ImplementedBy<EmptyConsumersMapping>().LifestyleSingleton());
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