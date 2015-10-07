using Aquarium.GiftShop.CastleWindsor;
using Aquarium.Octopus;

namespace OctopusDemoService
{
    class Program
    {
        static void Main(string[] args)
        {
            Container.Install(new OctopusDemoServiceContainerInstaller());
            var serviceDescription = "Octopus Demo Service. Consume multiple RabbitMQ events with different handlers";
            var serviceName = "Aquarium.Octopus";
            var serviceDisplayName = "Aquarium.Octopus";

            OctopusSetupFactory.SetupService(serviceDescription, serviceDisplayName, serviceName);
        }
    }
}
