using Topshelf.Quartz;

namespace Aquarium.TriggerFish
{
    public static class TriggerFishExtensionMethods
    {
        public static void AddTriggers(this QuartzConfigurator quartzConfigurator, IHaveLotsOfTriggers triggers)
        {
            foreach (var trigger in triggers.GetTriggers())
            {
                quartzConfigurator.AddTrigger(() => trigger);
            }
        }
    }
}