using System.Collections.Generic;

namespace Aquarium.GiftShop.RabbitMQ
{
    public interface IMapRabbitMqConsumers
    {
        Dictionary<string, RabbitMqConsumerConfiguration> Mappings { get; }
    }
}