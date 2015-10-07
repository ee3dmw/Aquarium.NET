using EasyNetQ;
using Quartz;

namespace SeaSnakeDemoService
{
    public class FeedingTimeJob : IJob
    {
        private readonly IBus _bus;

        public FeedingTimeJob(IBus bus)
        {
            _bus = bus;
        }

        public void Execute(IJobExecutionContext context)
        {
            _bus.Publish(new FeedingTimeMessage() { Id = 1 }, "FeedingTimeMessage");
        }
    }
}