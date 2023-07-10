using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptaEvents.Module.BusinessObjects
{
    [NavigationItem("Fields")]
    public class Field : BaseObject
    {
        public virtual string Name { get; set; }

        // fixed definition of allowed field types
        public virtual FieldType Type { get; set; }

        // zero-ranked display order of item
        public virtual int SortOrder { get; set; }

        // one to many
        public virtual Tab Tab { get; set; }
    }

    public enum FieldType 
    { 
        String, 
        Number, 
        Date, 
        Memo,
        Boolean 
    }
}
