﻿using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptaEvents.Module.BusinessObjects
{
    // class to keep the special fields for an event
    [NavigationItem(false)]
    public class EventField : BaseObject
    {
        public virtual string Field { get; set; }

        public virtual string Value { get; set; }

        [JsonIgnore]
        public virtual Event Event { get; set; }

        public virtual Tab Tab { get; set; }

        public virtual Guid TabID { get; set; }
    }
}
