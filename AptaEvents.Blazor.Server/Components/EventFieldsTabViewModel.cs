using AptaEvents.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;

namespace AptaEvents.Blazor.Server.Components
{
    public class EventFieldsTabViewModel  : ComponentModelBase
    {
        public IList<EventField> Data
        {
            get => GetPropertyValue<IList<EventField>>();
            set => SetPropertyValue(value);
        }

        public IEnumerable<TabWithEventFieldsViewModel> Tabs
        {
            get => GetPropertyValue<IEnumerable<TabWithEventFieldsViewModel>>();
            set => SetPropertyValue(value);
        }

        public Event Event
        {
            get => GetPropertyValue<Event>();
            set => SetPropertyValue(value);
        }

        public CollectionSourceBase CollectionSource
        {
            get => GetPropertyValue<CollectionSourceBase>();
            set => SetPropertyValue(value);
        }

        public void Refresh() => RaiseChanged();
        public void OnItemClick(SimpleEventFieldViewModel item) =>
            ItemClick?.Invoke(this, new EventFieldTabViewModelItemClickEventArgs(item));
        public event EventHandler<EventFieldTabViewModelItemClickEventArgs> ItemClick;
    }

    public class EventFieldTabViewModelItemClickEventArgs : EventArgs
    {
        public EventFieldTabViewModelItemClickEventArgs(SimpleEventFieldViewModel item)
        {
            Item = item;
        }

        public SimpleEventFieldViewModel Item { get; }
    }

    public class TabWithEventFieldsViewModel
    {
        public string Name { get; set; }

        public List<EventFieldViewModel> Fields { get; set; }
    }

    public class EventFieldViewModel
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public string Value { get; set; }

        public DateTime? Expiry { get; set; }

        public string Tab { get; set; }

        public DateTime? DateValue { get => DateTime.TryParse(Value, out var d) ? d : DateTime.Now; set => Value = value.ToString(); }
        public bool BooleanValue { get => bool.TryParse(Value , out var i) ? i : false; set => Value = value.ToString(); }
        public int NumberValue { get => int.TryParse(Value , out var i) ? i : 0; set => Value = value.ToString(); }
        public string StringValue { get => Value; set => Value = value; }
    }

    public class SimpleEventFieldViewModel
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public DateTime? Expiry { get; set; }
    }
}
