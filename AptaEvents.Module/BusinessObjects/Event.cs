using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Xpo;
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

        [Browsable(false)]
        public virtual string EventLink { get; set; }

        private EventLinkPropertyWrapper _EventLinkPropertyWrapper;
        [XafDisplayName("EventLink")]
        [DataSourceProperty(nameof(EventLinkDataSource))]
        [NotMapped]
        public EventLinkPropertyWrapper EventLinkWrapper
        {
            get
            {
                if (_EventLinkPropertyWrapper == null || _EventLinkPropertyWrapper.Key != EventLink)
                {
                    _EventLinkPropertyWrapper = EventLinkDataSource.FirstOrDefault(i => i.Key == EventLink);
                }
                return _EventLinkPropertyWrapper;
            }
            set
            {
                _EventLinkPropertyWrapper = value;
                EventLink = value.Key;
            }
        }

        [System.ComponentModel.DisplayName("Fields")]
        public virtual IList<EventField> EventFields { get; set; } = new ObservableCollection<EventField>();

        private BindingList<EventLinkPropertyWrapper> _eventLinkDataSource;
        [Browsable(false)]
        [NotMapped]
        public BindingList<EventLinkPropertyWrapper> EventLinkDataSource
        {
            get
            {
                if (_eventLinkDataSource == null)
                {
                    _eventLinkDataSource = new BindingList<EventLinkPropertyWrapper>();
                    for (int i = 0; i < 5; i++)
                    {
                        _eventLinkDataSource.Add(new EventLinkPropertyWrapper("Position" + i.ToString(), i.ToString()));
                    }
                }
                return _eventLinkDataSource;
            }
        }
    }

    public class ApiEventList
    {
        public int eventID { get; set; } = 0;
        public string eventName { get; set; } = string.Empty;
    }

    [DomainComponent, XafDefaultProperty(nameof(EventLinkName))]
    public class EventLinkPropertyWrapper
    {
        private string _key;
        private string _eventLinkName;

        public EventLinkPropertyWrapper(string eventLinkName, string key)
        {
            this._key = key;
            this._eventLinkName = eventLinkName;
        }
        
        [DevExpress.ExpressApp.Data.Key]
        public string Key { get { return _key; }}
        public string EventLinkName { get { return _eventLinkName; } }
    }
}
