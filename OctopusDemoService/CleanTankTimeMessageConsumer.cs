using System;
using System.Threading;
using Aquarium.GiftShop.RabbitMQ;

namespace OctopusDemoService
{
    public class CleanTankTimeMessageConsumer : MessageConsumer<CleanTankTimeMessage>
    {
        public override void Consume(CleanTankTimeMessage message)
        {
            //Thread.Sleep(20000);
            Console.WriteLine("Cleaning Tanks Time! Tank {0} on Thread {1} at {2}", message.Id, Thread.CurrentThread.ManagedThreadId, DateTime.Now);
        }
    }
}