using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TriggerFishDemoService
{
    public class TriggerFishDemoServiceContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<FeedingTimeJob>().LifestyleTransient());
        }
    }
}