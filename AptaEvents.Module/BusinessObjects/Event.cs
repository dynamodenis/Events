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

        private PositionPropertyWrapper _positionPropertyWrapper;
        [XafDisplayName("EventLink")]
        [DataSourceProperty(nameof(PositionDataSource))]
        [NotMapped]
        public PositionPropertyWrapper PositionWrapper
        {
            get
            {
                if (_positionPropertyWrapper == null || _positionPropertyWrapper.Key != EventLink)
                {
                    _positionPropertyWrapper = PositionDataSource.FirstOrDefault(i => i.Key == EventLink);
                }
                return _positionPropertyWrapper;
            }
            set
            {
                _positionPropertyWrapper = value;
                EventLink = value.Key;
            }
        }

        [System.ComponentModel.DisplayName("Fields")]
        public virtual IList<EventField> EventFields { get; set; } = new ObservableCollection<EventField>();

        private BindingList<PositionPropertyWrapper> _positionDataSource;
        [Browsable(false)]
        [NotMapped]
        public BindingList<PositionPropertyWrapper> PositionDataSource
        {
            get
            {
                if (_positionDataSource == null)
                {
                    _positionDataSource = new BindingList<PositionPropertyWrapper>();
                    for (int i = 0; i < 5; i++)
                    {
                        _positionDataSource.Add(new PositionPropertyWrapper("Position" + i.ToString(), i.ToString()));
                    }
                }
                return _positionDataSource;
            }
        }
    }

    public class ApiEventList
    {
        public int eventID { get; set; } = 0;
        public string eventName { get; set; } = string.Empty;
    }

    [DomainComponent, XafDefaultProperty(nameof(PositionName))]
    public class PositionPropertyWrapper
    {
        private string _key;
        private string _positionName;

        public PositionPropertyWrapper(string positionName, string key)
        {
            this._key = key;
            this._positionName = positionName;
        }
        
        [DevExpress.ExpressApp.Data.Key]
        public string Key { get { return _key; }}
        public string PositionName { get { return _positionName; } }
    }
}
