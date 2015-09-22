using System.Collections.Generic;
using Quartz;

namespace Aquarium.TriggerFish
{
    public interface IHaveLotsOfTriggers
    {
        List<ITrigger> GetTriggers();
    }
}