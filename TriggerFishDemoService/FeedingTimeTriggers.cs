using System.Collections.Generic;
using Aquarium.TriggerFish;
using Quartz;

namespace TriggerFishDemoService
{
    public class FeedingTimeTriggers : IHaveLotsOfTriggers
    {
        public List<ITrigger> GetTriggers()
        {
            var triggers = new List<ITrigger>();
            triggers.Add(TriggerBuilder.Create().WithSimpleSchedule(s => s.WithIntervalInSeconds(5).RepeatForever().Build()).UsingJobData("Food", "Fish Eggs").Build());
            triggers.Add(TriggerBuilder.Create().WithSimpleSchedule(s => s.WithIntervalInSeconds(20).RepeatForever().Build()).UsingJobData("Food", "Eels").Build());
            return triggers;
        }
    }
}