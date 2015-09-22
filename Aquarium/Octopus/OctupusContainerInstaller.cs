using Aquarium.GiftShop.EasyNetQ;
using Aquarium.GiftShop.RabbitMQ;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ;

namespace Aquarium.Octopus
{
    public class OctupusContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMapOctopusConsumers>().ImplementedBy<MapOctopusConsumers>().LifestyleSingleton());
            container.Register(Component.For<OctopusService>().LifestyleTransient());
            container.Register(Component.For<IBus>().UsingFactoryMethod(BusBuilder.CreateMessageBus).LifestyleSingleton());
        }
    }
}