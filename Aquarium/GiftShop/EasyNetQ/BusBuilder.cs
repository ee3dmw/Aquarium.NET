using System;
using System.Configuration;
using EasyNetQ;

namespace Aquarium.GiftShop.EasyNetQ
{
    public class BusBuilder
    {
        public static IBus CreateMessageBus(Action<IServiceRegister> registerServices)
        {
            var connectionString = ConfigurationManager.AppSettings["rabbitConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException("rabbitconnectionstring is missing or empty");
            }

            return RabbitHutch.CreateBus(connectionString,
                registerServices);
        }
    }
}