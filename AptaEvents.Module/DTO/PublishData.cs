using AptaEvents.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptaEvents.Module.DTO
{
    public class PublishData
    {
        public virtual IList<PublishTab> Tabs { get; set; }
    }

    public class PublishTab
    {
        public virtual string Name { get; set; }
        public virtual IList<EventField> Fields { get; set; }
    }
}
