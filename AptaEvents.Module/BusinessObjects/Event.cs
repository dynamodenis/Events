using AptaEvents.Common.Helpers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using AptaEvents.Module.DTO;
using DevExpress.ExpressApp.ConditionalAppearance;
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
using System.Security.AccessControl;


namespace AptaEvents.Module.BusinessObjects
{
    [NavigationItem("Events")]
    public class Event : BaseObject
    {
        private AptaEventsApi _eventsApi;
        private IConfiguration _configuration;

        private EventLinkPropertyWrapper _EventLinkPropertyWrapper;
        private BindingList<EventLinkPropertyWrapper> _eventLinkDataSource;

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public virtual int? EventId { get; set; }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public virtual int? SeasonId { get; set; }

        public virtual string Name { get; set; }

		[Column(TypeName = "date")]
		public virtual DateTime Date { get; set; }

		public virtual bool Live { get; set; }

        [Browsable(false)]
        public virtual string EventLink { get; set; }

        [Appearance("Event.PublishData.Hide", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Column(TypeName = "jsonb")]
        public virtual PublishData PublishData { get; set; }

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
                if (ObjectSpace != null && (_EventLinkPropertyWrapper == null || _EventLinkPropertyWrapper.Key != EventLink))
                {
                    _EventLinkPropertyWrapper = EventLinkDataSource.FirstOrDefault(i => i.Key == EventLink);
                }
                return _EventLinkPropertyWrapper;
            }
            set
            {
                _EventLinkPropertyWrapper = value;
                EventLink = value?.Key;

                PopulateAptaBaseValues(value?.Key);
            }
        }

        [Browsable(false)]
        [NotMapped]
        public BindingList<EventLinkPropertyWrapper> EventLinkDataSource
        {
            get
            {
                // check objectspace to make sure it doesn't load in the api
                if (_eventLinkDataSource == null && ObjectSpace != null)
                {
                    _eventLinkDataSource = new BindingList<EventLinkPropertyWrapper>();

                    // temporary assign past date
                    var date = _configuration.GetRequiredSection("AptaEventsIntegrationApi")?["StartDate"];
                    var dateParam = string.IsNullOrEmpty(date) ? null : date;

                    string eventResponse = null;
                    
                    try
                    {
                        eventResponse = _eventsApi.GetRequest("Tournaments/GetTournamentList", $"date={dateParam}");
                        var events = JsonConvert.DeserializeObject<List<TournamentListingDto>>(eventResponse);

                        foreach (var e in events)
                        {
                            _eventLinkDataSource.Add(new EventLinkPropertyWrapper(e.TournamentName, e.TournamentID.ToString()));
                        }
                    }
                    catch (Exception e)
                    {
                        Tracing.Tracer.LogText($"GetTournamentList: {eventResponse}");
                        Tracing.Tracer.LogError(e);
                    }

                }
                return _eventLinkDataSource;
            }
        }


        private void PopulateAptaBaseValues(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            try
            {
                var eventResponse = _eventsApi.GetRequest($"Tournaments/GetTournamentData/{key}");
                var eventData = JsonConvert.DeserializeObject<TournamentDataDto>(eventResponse);

                this.EventId = eventData.EventId;
                this.SeasonId = eventData.SeasonId;

                SetEventField("AptaTourFlag", eventData.AptaTourFlag.ToString());
                SetEventField("CancelledFlag", eventData.CancelledFlag.ToString());
                SetEventField("Capacity", eventData.Capacity.ToString());
                SetEventField("EndDate", eventData.EndDate.ToString());
                SetEventField("EntryCloseDate", eventData.EntryCloseDate.ToString());
                SetEventField("EntryOpenDate", eventData.EntryOpenDate.ToString());
                SetEventField("EntryOpenFlag", eventData.EntryOpenFlag.ToString());
                SetEventField("EventScoringFlag", eventData.EventScoringFlag.ToString());
                SetEventField("GrandPrixFlag", eventData.GrandPrixFlag.ToString());
                SetEventField("JuniorFlag", eventData.JuniorFlag.ToString());
                SetEventField("MastersFlag", eventData.MastersFlag.ToString());
                SetEventField("NationalChampionshipFlag", eventData.NationalChampionshipFlag.ToString());
                SetEventField("NRTFlag", eventData.NRTFlag.ToString());
                SetEventField("PTIFlag", eventData.PTIFlag.ToString());
                SetEventField("Region", eventData.Region.ToString());
                SetEventField("SeasonName", eventData.SeasonName.ToString());
                SetEventField("ShowWaitingListFlag", eventData.ShowWaitingListFlag.ToString());
                SetEventField("StartDate", eventData.StartDate.ToString());
                SetEventField("TournamentScoringFlag", eventData.TournamentScoringFlag.ToString());
                SetEventField("TournamentType", eventData.TournamentType.ToString());
			}
            catch (Exception e)
            {
                Tracing.Tracer.LogError(e);
            }
        }

		private void SetEventField(string field, string value)
		{
			var eventField = EventFields.FirstOrDefault(f => f.Field == field);

            if (eventField != null)
            {
                eventField.Value = value;
            }
			else
			{
				eventField = ObjectSpace.CreateObject<EventField>();

				eventField.Field = field;
				eventField.Value = value;

                EventFields.Add(eventField);
			}
		}
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
