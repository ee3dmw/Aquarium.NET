using Aquarium.GiftShop.RabbitMQ;
using EasyNetQ;

namespace OctopusDemoService
{
    [Queue("CleanTankTimeMessage", ExchangeName = Constants.DefaultRabbitMqExchangeName)]
    public class CleanTankTimeMessage : Message
    {
        public int Id { get; set; }
    }
}