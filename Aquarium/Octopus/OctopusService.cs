using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.RabbitMQ;
using EasyNetQ;
using EasyNetQ.NonGeneric;

namespace Aquarium.Octopus
{
    public class OctopusService
    {
        private readonly IBus _bus;
        private readonly IMapRabbitMqConsumers _mapRabbitMqConsumers;
        private readonly List<IDisposable> _subscriptionsToDispose = new List<IDisposable>();
        private readonly CancellationTokenSource _cancellationToken;
        private static int _tasksInProgressCount = 0;

        public OctopusService(IBus bus, IMapRabbitMqConsumers mapRabbitMqConsumers)
        {
            _bus = bus;
            _mapRabbitMqConsumers = mapRabbitMqConsumers;
            _cancellationToken = new CancellationTokenSource();
        }

        public bool Start()
        {
            foreach (var mapping in _mapRabbitMqConsumers.Mappings)
            {
                var result = _bus.SubscribeAsync(mapping.Value.EventType, mapping.Value.SubscriptionId, b => StartNew((Message)b, mapping.Value, _cancellationToken), sub => sub.WithTopic(mapping.Value.Topic));
                _subscriptionsToDispose.Add(result);
            }

            return true;
        }

        public bool Stop()
        {
            Console.WriteLine("STOPPING");
            _cancellationToken.Cancel();

            WaitForMessagesInProgressToFinish();

            // kill all subscribers connection to rabbit
            foreach (var cancellationToken in _subscriptionsToDispose)
                cancellationToken.Dispose();

            return true;
        }

        private static void WaitForMessagesInProgressToFinish()
        {
            int ducks = 0;
            while (ducks < 20)
            {
                if (_tasksInProgressCount == 0)
                    break;

                Thread.Sleep(500);
                ducks++;
            }
        }

        private static Task StartNew(Message message, RabbitMqConsumerConfiguration config, CancellationTokenSource cancellationToken)
        {
            Console.WriteLine("Starting new task, thread " + Thread.CurrentThread.ManagedThreadId);
            var task = Task.Factory.StartNew(() =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    // sleep forever so we don't do any more processing
                    Thread.Sleep(System.Threading.Timeout.Infinite);
                }

                _tasksInProgressCount++;
                Console.WriteLine("SETUP THREAD " + Thread.CurrentThread.ManagedThreadId);
                var handler = (IConsumeMessages)Container.Resolve(config.HandlerType);
                handler.OnConsume(message);
            }).ContinueWith(t => _tasksInProgressCount--);

            return task;
        }
    }
}