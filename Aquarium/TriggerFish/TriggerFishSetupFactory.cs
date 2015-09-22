using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aquarium.GiftShop.CastleWindsor;
using Aquarium.GiftShop.TopShelf;
using Quartz;
using Quartz.Impl;
using Topshelf;
using Topshelf.Quartz;
using Topshelf.ServiceConfigurators;

namespace Aquarium.TriggerFish
{
    public class TriggerFishSetupFactory
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static int SetupService<T>(JobDataMap jobData, IHaveLotsOfTriggers triggers)
        {
            Container.Install(new TriggerFishContainerInstaller());
           
            NameValueCollection quartzConfig =
                        (NameValueCollection)ConfigurationManager.GetSection("quartz");

            string jobName = jobData.GetString("JobName");
            string jobDesc = jobData.GetString("JobDescription");
            JobKey jobKey = new JobKey(jobName);

            TopshelfExitCode exitCode = HostFactory.Run(x =>
            {
                x.UseLog4Net();

                x.Service((Action<ServiceConfigurator<EmptyTopshelfService>>)(s =>
                {
                    s.BeforeStoppingService(c =>
                    {
                        _log.InfoFormat("Shutting down service: {0}", jobName);
                        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                        scheduler.Interrupt(jobKey);
                    });
                    s.ConstructUsing(() => new EmptyTopshelfService());
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());

                    // Pass in a StdSchedularFactory that takes Quartz onfiguration from XML config
                    ScheduleJobServiceConfiguratorExtensions.SchedulerFactory =
                        () =>
                        {
                            var sched = new StdSchedulerFactory(quartzConfig);
                            return sched.GetScheduler();
                        };

                    s.UsingQuartzJobFactory(() => new WindsorJobFactory(Container.GetContainer()));
                    s.ScheduleQuartzJob(q => q.WithJob(
                                                            () => JobBuilder.Create(typeof(T))
                                                                            .WithIdentity(jobKey)
                                                                            .UsingJobData(jobData)
                                                                            .Build()
                                                        )
                                                        .AddTriggers(triggers));
                }));

                x.SetDisplayName(jobName);
                x.SetServiceName(jobName);
                x.SetDescription(jobDesc);

                //x.RunAs(serviceUsername, servicePassword);
                x.RunAsLocalSystem();
            });

            Environment.ExitCode = (int)exitCode;
            return (int)exitCode;
        }
    }
}
