namespace Aquarium.GiftShop.RabbitMQ
{
    public interface IConsumeMessages
    {
        void OnConsume(Message rawMessage);
    }
}