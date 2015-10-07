using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Aquarium.GiftShop.RabbitMQ;
using Aquarium.Octopus;
using EasyNetQ;

namespace Aquarium.GiftShop.EasyNetQ
{
    public class ClassNameOnlyTypeSerializer : ITypeNameSerializer
    {
        private readonly Dictionary<string, Type> _deserializedTypes = new Dictionary<string, Type>();

        public ClassNameOnlyTypeSerializer(IMapRabbitMqConsumers consumerConfigurations)
        {
            foreach (var consumerConfiguration in consumerConfigurations.Mappings)
            {
                var key = consumerConfiguration.Value.EventType.Name;
                if (!_deserializedTypes.ContainsKey(key))
                    _deserializedTypes.Add(key, consumerConfiguration.Value.EventType);
            }
        }
        
        public Type DeSerialize(string typeName)
        {
            Type t = _deserializedTypes[typeName];
            if (t == null)
                throw new EasyNetQException("No matching type for event type string " + typeName);

            return t;
        }

        private readonly ConcurrentDictionary<Type, string> _serializedTypes = new ConcurrentDictionary<Type, string>();

        public string Serialize(Type type)
        {
            return _serializedTypes.GetOrAdd(type, t =>
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