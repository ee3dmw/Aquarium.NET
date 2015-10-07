using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace OctopusDemoService
{
    public class OctopusDemoServiceContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<FeedingTimeMessageConsumer>().LifestyleTransient());
            container.Register(Component.For<CleanTankTimeMessageConsumer>().LifestyleTransient());
        }
    }
}