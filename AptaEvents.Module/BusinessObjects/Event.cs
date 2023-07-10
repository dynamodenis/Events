using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptaEvents.Module.BusinessObjects
{
    [NavigationItem("Events")]
    public class Event : BaseObject
    {
        public virtual string Name { get; set; }

        [Column(TypeName = "date")]
        public virtual DateTime Date { get; set; }

        public virtual bool Live { get; set; }

        public virtual string EventLink { get; set; }

        [DisplayName("Fields")]
        public virtual IList<EventField> EventFields { get; set; } = new ObservableCollection<EventField>();
    }

    public class ApiEventList
    {
        public int eventID { get; set; } = 0;
        public string eventName { get; set; } = string.Empty;
    }

}
