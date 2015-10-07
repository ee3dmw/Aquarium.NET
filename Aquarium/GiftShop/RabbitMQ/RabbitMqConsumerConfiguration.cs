using System;

namespace Aquarium.GiftShop.RabbitMQ
{
    public class RabbitMqConsumerConfiguration
    {
        public RabbitMqConsumerConfiguration(string subscriptionid, Type handler, Type eventType, string topic)
        {
            SubscriptionId = subscriptionid;
            HandlerType = handler;
            EventType = eventType;
            Topic = topic;
        }

        public string SubscriptionId { get; set; }
        public Type HandlerType { get; set; }
        public Type EventType { get; set; }
        public string Topic { get; set; }
    }
}