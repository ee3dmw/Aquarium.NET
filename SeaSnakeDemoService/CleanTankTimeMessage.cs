using Aquarium.GiftShop.RabbitMQ;
using EasyNetQ;

namespace SeaSnakeDemoService
{
    [Queue("CleanTankTimeMessage", ExchangeName = Constants.DefaultRabbitMqExchangeName)]
    public class CleanTankTimeMessage : Message
    {
        public int Id { get; set; }
    }
}