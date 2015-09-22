using System;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.RabbitMQ;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace Aquarium.Octopus
{
    public class OctopusSetupFactory
    {
        public static void SetupService(string serviceDescription, string serviceDisplayName, string serviceName)
        {
            Container.Install(new OctupusContainerInstaller());

            HostFactory.Run(x =>
            {
                x.Service((Action<ServiceConfigurator<OctopusService>>)(s =>
                {
                    s.ConstructUsing(name => Container.Resolve<OctopusService>());
                    s.WhenStarted((tc, control) => tc.Start());
                    s.WhenStopped((tc, control) =>
                    {
                        tc.Stop();
                        Container.Release(tc);
                        Container.Dispose();
                        return true;
                    });
                }));

                x.RunAsLocalSystem();


                x.SetDescription(serviceDescription);
                x.SetDisplayName(serviceDisplayName);
                x.SetServiceName(serviceName);
            });
        }
    }
}