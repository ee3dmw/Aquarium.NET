using System;
using Quartz;

namespace Aquarium.GiftShop.Quartz
{
    public class JobConfig
    {
        public Type JobType { get; set; }
        public string JobDescription { get; set; }
        public ITrigger JobTrigger { get; set; }
    }
}