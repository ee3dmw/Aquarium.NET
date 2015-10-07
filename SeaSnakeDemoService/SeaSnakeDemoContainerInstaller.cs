using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SeaSnakeDemoService
{
    public class SeaSnakeDemoContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<FeedingTimeJob>().LifestyleTransient());
            container.Register(Component.For<CleanTankJob>().LifestyleTransient());
        }
    }
}