using System;
using System.Collections.Generic;

namespace Aquarium.GiftShop.RabbitMQ
{
    public class Message
    {
    }

    public class ConsumerConfiguration
    {
        public ConsumerConfiguration(string subscriptionid, Type handler, Type eventType, string topic)
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

    public interface IMapOctopusConsumers
    {
        Dictionary<string, ConsumerConfiguration> Mappings { get; }
    }

    public class MapOctopusConsumers : IMapOctopusConsumers
    {
        private Dictionary<string, ConsumerConfiguration> _mappings =
            new Dictionary<string, ConsumerConfiguration>();

        public MapOctopusConsumers()
        {
            //string handlerType = "TestConsumer.IHandleRabbits`1[[TestConsumer.EasyMail, TestConsumer]]";
            string handlerType = "TestConsumer.EasyMailMessageHandler, TestConsumer";
            Type type = Type.GetType(handlerType);
            string eventTypeString = "TestConsumer.EasyMail, TestConsumer";
            Type eventType = Type.GetType(eventTypeString);
            _mappings.Add("easy mail queue 1", new ConsumerConfiguration("subscriptionid", type, eventType, "EasyMail"));
            _mappings.Add("easy mail queue 2", new ConsumerConfiguration("subscriptionid", type, eventType, "EasyMail"));

            string handlerType2 = "TestConsumer.EasyMailMessageHandler2, TestConsumer";
            Type type2 = Type.GetType(handlerType2);
            string eventTypeString2 = "TestConsumer.EasyMail2, TestConsumer";
            Type eventType2 = Type.GetType(eventTypeString2);
            _mappings.Add("easy mail queue 3", new ConsumerConfiguration("subscriptionid", type2, eventType2, "EasyMail2"));
        }

        public Dictionary<string, ConsumerConfiguration> Mappings { get { return _mappings; } }
    }
}
