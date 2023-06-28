using DevExpress.Persistent.BaseImpl.EF;
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
    public class EventField : BaseObject
    {
        public virtual string Field { get; set; }

        public virtual string Value { get; set; }
    }
}
