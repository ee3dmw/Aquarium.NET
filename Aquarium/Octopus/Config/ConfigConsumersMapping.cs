using System;
using System.Collections.Generic;
using System.Configuration;
using Aquarium.GiftShop.RabbitMQ;

namespace Aquarium.Octopus.Config
{
    public class ConfigConsumersMapping : IMapRabbitMqConsumers
    {
        private Dictionary<string, RabbitMqConsumerConfiguration> _mappings =
            new Dictionary<string, RabbitMqConsumerConfiguration>();

        public ConfigConsumersMapping()
        {
            OctopusConfigSettings config = new OctopusConfigSettings();
            foreach (var mapping in config.ConsumerMappings)
            {
                Type consumerType = Type.GetType(mapping.EventConsumerType);
                Type eventType = Type.GetType(mapping.EventType);
                _mappings.Add(mapping.name, new RabbitMqConsumerConfiguration(mapping.SubscriptionId, consumerType, eventType, eventType.Name));
            }
        }

        public Dictionary<string, RabbitMqConsumerConfiguration> Mappings { get { return _mappings; } }
    }

    public class OctopusSection : ConfigurationSection
    {
        [ConfigurationProperty("consumerMappings")]
        public ConsumerMappingsCollection ConsumerMappings
        {
            get { return ((ConsumerMappingsCollection)(base["consumerMappings"])); }
            set { base["consumerMappings"] = value; }
        }
        
    }

    [ConfigurationCollection(typeof(ConsumerMapping))]
    public class ConsumerMappingsCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "mapping";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }


        public override bool IsReadOnly()
        {
            return false;
        }


        protected override ConfigurationElement CreateNewElement()
        {
            return new ConsumerMapping();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConsumerMapping)(element)).name;
        }

        public ConsumerMapping this[int idx]
        {
            get
            {
                return (ConsumerMapping)BaseGet(idx);
            }
        }
    }

    public class ConsumerMapping : ConfigurationElement
    {

        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("eventType", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string EventType
        {
            get { return (string)base["eventType"]; }
            set { base["eventType"] = value; }
        }

        [ConfigurationProperty("eventConsumerType", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string EventConsumerType
        {
            get { return (string)base["eventConsumerType"]; }
            set { base["eventConsumerType"] = value; }
        }

        [ConfigurationProperty("subscriptionId", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string SubscriptionId
        {
            get { return (string)base["subscriptionId"]; }
            set { base["subscriptionId"] = value; }
        }
    }

    public class OctopusConfigSettings
    {
        public OctopusSection OctopusSection
        {
            get
            {
                return (OctopusSection)ConfigurationManager.GetSection("aquarium/octopus");
            }
        }

        public ConsumerMappingsCollection ConsumerMappingsCollection
        {
            get
            {
                return this.OctopusSection.ConsumerMappings;
            }
        }

        public IEnumerable<ConsumerMapping> ConsumerMappings
        {
            get
            {
                foreach (ConsumerMapping selement in this.ConsumerMappingsCollection)
                {
                    if (selement != null)
                        yield return selement;
                }
            }
        }
    }
}