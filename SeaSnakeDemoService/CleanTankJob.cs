using EasyNetQ;
using Quartz;

namespace SeaSnakeDemoService
{
    public class CleanTankJob : IJob
    {
        private readonly IBus _bus;

        public CleanTankJob(IBus bus)
        {
            _bus = bus;
        }

        public void Execute(IJobExecutionContext context)
        {
            _bus.Publish(new CleanTankTimeMessage(), "CleanTankTimeMessage");
        }
    }
}