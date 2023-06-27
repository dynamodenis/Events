using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptaEvents.Module.BusinessObjects
{
    [NavigationItem("Events")]
    public class Event : BaseObject
    {
        public virtual string Name { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual bool Live { get; set; }

        public virtual string EventLink { get; set; }

        [DisplayName("Fields")]
        [Column(TypeName = "jsonb")]
        public virtual IList<EventField> EventFields { get; set; }
    }
}
