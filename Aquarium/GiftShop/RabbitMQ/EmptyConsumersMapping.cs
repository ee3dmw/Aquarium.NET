using System.Collections.Generic;

namespace Aquarium.GiftShop.RabbitMQ
{
    public class EmptyConsumersMapping : IMapRabbitMqConsumers
    {
        private readonly Dictionary<string, RabbitMqConsumerConfiguration> _emptyMappings = new Dictionary<string, RabbitMqConsumerConfiguration>();

        public Dictionary<string, RabbitMqConsumerConfiguration> Mappings
        {
            get { return _emptyMappings; }
        }
    }
}
