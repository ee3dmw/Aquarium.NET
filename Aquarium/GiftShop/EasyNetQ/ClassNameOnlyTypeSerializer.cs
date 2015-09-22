using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Aquarium.GiftShop.RabbitMQ;
using EasyNetQ;

namespace Aquarium.GiftShop.EasyNetQ
{
    public class ClassNameOnlyTypeSerializer : ITypeNameSerializer
    {
        private readonly Dictionary<string, Type> deserializedTypes = new Dictionary<string, Type>();

        public ClassNameOnlyTypeSerializer(List<ConsumerConfiguration> configuration)
        {
            if (configuration == null)
                return;

            foreach (var consumerConfiguration in configuration)
            {
                var key = consumerConfiguration.EventType.Name;
                if (!deserializedTypes.ContainsKey(key))
                    deserializedTypes.Add(key, consumerConfiguration.EventType);
            }
        }

        public Type DeSerialize(string typeName)
        {
            Type t = deserializedTypes[typeName];
            if (t == null)
                throw new EasyNetQException("No matching type for event type string " + typeName);

            return t;
        }

        private readonly ConcurrentDictionary<Type, string> serializedTypes = new ConcurrentDictionary<Type, string>();

        public string Serialize(Type type)
        {
            return serializedTypes.GetOrAdd(type, t =>
            {
                var typeName = t.Name;
                if (typeName.Length > 255)
                {
                    throw new EasyNetQException("The serialized name of type '{0}' exceeds the AMQP " +
                                                "maximum short string length of 255 characters.", t.Name);
                }
                return typeName;
            });
        }
    }
}