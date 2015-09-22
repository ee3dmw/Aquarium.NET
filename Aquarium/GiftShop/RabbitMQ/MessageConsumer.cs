namespace Aquarium.GiftShop.RabbitMQ
{
    public abstract class MessageConsumer<T> : IConsumeMessages where T : Message
    {
        public T Cast(Message rawMessage)
        {
            return (T)rawMessage;
        }


        public void OnConsume(Message rawMessage)
        {
            var message = Cast(rawMessage);
            Consume(message);
        }

        public abstract void Consume(T message);
    }
}