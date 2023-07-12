using AptaEvents.Common.Helpers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Xpo;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace AptaEvents.Module.BusinessObjects
{
    [NavigationItem("Events")]
    public class Event : BaseObject
    {
        private AptaEventsApi _eventsApi;
        private IConfiguration _configuration;

        private EventLinkPropertyWrapper _EventLinkPropertyWrapper;
        private BindingList<EventLinkPropertyWrapper> _eventLinkDataSource;

        public virtual string Name { get; set; }

        [Column(TypeName = "date")]
        public virtual DateTime Date { get; set; }

        public virtual bool Live { get; set; }

        [Browsable(false)]
        public virtual string EventLink { get; set; }

        [System.ComponentModel.DisplayName("Fields")]
        public virtual IList<EventField> EventFields { get; set; } = new ObservableCollection<EventField>();

        public override void OnCreated()
        {
            base.OnCreated();

            if (_eventsApi == null)
            {
                _configuration = ObjectSpace.ServiceProvider.GetRequiredService<IConfiguration>();

                _eventsApi = new AptaEventsApi(_configuration);
            }
        }

        public override void OnLoaded()
        {
            base.OnCreated();

            if (_eventsApi == null)
            {
                _configuration = ObjectSpace.ServiceProvider.GetRequiredService<IConfiguration>();

                _eventsApi = new AptaEventsApi(_configuration);
            }
        }

        [XafDisplayName("EventLink")]
        [DataSourceProperty(nameof(EventLinkDataSource))]
        [NotMapped]
        // need to hide from list view otherwise it will call api per item
        [VisibleInListView(false)]
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

        [Browsable(false)]
        [NotMapped]
        public BindingList<EventLinkPropertyWrapper> EventLinkDataSource
        {
            get
            {
                if (_eventLinkDataSource == null)
                {
                    _eventLinkDataSource = new BindingList<EventLinkPropertyWrapper>();

                    // temporary assign past date
                    var date = _configuration.GetRequiredSection("AptaEventsIntegrationApi")?["StateDate"];
                    var dateParam = string.IsNullOrEmpty(date) ? null : date;
                    
                    try
                    {
                        var eventResponse = _eventsApi.GetRequest("Events/GetEventList", $"date={dateParam}");
                        var events = JsonConvert.DeserializeObject<List<ApiEventListing>>(eventResponse);

                        foreach (var e in events)
                        {
                            _eventLinkDataSource.Add(new EventLinkPropertyWrapper(e.eventName, e.eventID.ToString()));
                        }
                    }
                    catch (Exception e)
                    {
                        Tracing.Tracer.LogError(e);
                    }

                }
                return _eventLinkDataSource;
            }
        }
    }

    public class ApiEventListing
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
