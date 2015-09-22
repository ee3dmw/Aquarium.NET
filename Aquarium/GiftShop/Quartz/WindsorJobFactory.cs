using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Quartz;
using Quartz.Spi;

namespace Aquarium.GiftShop.CastleWindsor
{
    public class WindsorJobFactory : IJobFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorJobFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle)
        {
            return (IJob)_container.Resolve(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {

        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_container.Resolve(bundle.JobDetail.JobType);
        }
    }
}
