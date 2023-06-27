using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptaEvents.Module.BusinessObjects
{
    [NavigationItem("Tabs")]
    public class Tab : BaseObject
    {
        public virtual string Name { get; set; }

        // zero-ranked display order of item
        public virtual int SortOrder { get; set; }

        // list of fields under each tab
        public virtual IList<Field> Fields { get; set; } = new ObservableCollection<Field>();
    }
}
