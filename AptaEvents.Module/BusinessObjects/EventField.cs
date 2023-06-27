using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptaEvents.Module.BusinessObjects
{
    // class to keep the special fields for an event
    [Browsable(false)]
    public class EventField
    {
        public virtual Field Field { get; set; }

        public virtual string Value { get; set; }
    }
}
