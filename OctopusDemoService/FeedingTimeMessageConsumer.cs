using System;
using System.Threading;
using Aquarium.GiftShop.RabbitMQ;

namespace OctopusDemoService
{
    public class FeedingTimeMessageConsumer : MessageConsumer<FeedingTimeMessage>
    {
        public override void Consume(FeedingTimeMessage message)
        {
            //Thread.Sleep(20000);
            Console.WriteLine("FeedingTimeMessage! Tank {0} on Thread {1} at {2}", message.Id, Thread.CurrentThread.ManagedThreadId, DateTime.Now);
        }
    }
}