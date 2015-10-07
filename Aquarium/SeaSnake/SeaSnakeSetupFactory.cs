using System;
using System.Collections.Generic;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.Quartz;
using Aquarium.GiftShop.TopShelf;
using Quartz;
using Topshelf;
using Topshelf.Quartz;
using Topshelf.ServiceConfigurators;

namespace Aquarium.SeaSnake
{
    public class SeaSnakeSetupFactory
    {
        public static void SetupService(string serviceDescription, string serviceDisplayName, string serviceName, List<JobConfig> jobConfigs)
        {
            Container.Install(new SeaSnakeContainerInstaller());

            HostFactory.Run(x =>
            {
                x.Service((Action<ServiceConfigurator<EmptyTopshelfService>>)(s =>
                {
                    //s.BeforeStoppingService(c =>
                    //{
                    //    _log.InfoFormat("Shutting down service: {0}", jobName);
                    //    IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                    //    scheduler.Interrupt(jobKey);
                    //});
                    s.ConstructUsing(() => new EmptyTopshelfService());
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) =>
                    {
                        service.Stop();
                        Container.Release(service);
                        Container.Dispose();
                        return true;
                    });

                    // Pass in a StdSchedularFactory that takes Quartz onfiguration from XML config
                    //ScheduleJobServiceConfiguratorExtensions.SchedulerFactory =
                    //    () =>
                    //    {
                    //        var sched = new StdSchedulerFactory(quartzConfig);
                    //        return sched.GetScheduler();
                    //    };

                    s.UsingQuartzJobFactory(() => new WindsorJobFactory(Container.GetContainer()));

                    foreach (var jobconfig in jobConfigs)
                    {
                        s.ScheduleQuartzJob(q => q.WithJob(() => JobBuilder.Create(jobconfig.JobType)
                            .WithIdentity(new JobKey(jobconfig.JobDescription))
                            .Build()
                            ).AddTrigger(() => jobconfig.JobTrigger));
                    }
                }));

                x.RunAsLocalSystem();

                x.SetDescription(serviceDescription);
                x.SetDisplayName(serviceDisplayName);
                x.SetServiceName(serviceName);
            });
        }
    }
}
