using System;
using System.Configuration;
using System.Linq;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.RabbitMQ;
using EasyNetQ;
using EasyNetQ.Loggers;

namespace Aquarium.GiftShop.EasyNetQ
{
    public class BusBuilder
    {
        public static IBus CreateMessageBus()
        {
            var connectionString = ConfigurationManager.AppSettings["rabbitConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException("rabbitconnectionstring is missing or empty");
            }

            return RabbitHutch.CreateBus(connectionString,
                x =>
                    x.Register<IEasyNetQLogger>(_ => new ConsoleLogger())
                        .Register<ITypeNameSerializer>(_ => new ClassNameOnlyTypeSerializer(Container.Resolve<IMapOctopusConsumers>().Mappings.Values.ToList())));
        }
    }
}