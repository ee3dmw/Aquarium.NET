using Aquarium.GiftShop.RabbitMQ;
using EasyNetQ;

namespace OctopusDemoService
{
    [Queue("FeedingTimeMessage", ExchangeName = Constants.DefaultRabbitMqExchangeName)]
    public class FeedingTimeMessage : Message
    {
        public int Id { get; set; }
    }
}