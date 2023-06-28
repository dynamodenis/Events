using AptaEvents.Module.BusinessObjects;
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

        public void Refresh() => RaiseChanged();
        public void OnItemClick(EventField item) =>
            ItemClick?.Invoke(this, new EventFieldTabViewModelItemClickEventArgs(item));
        public event EventHandler<EventFieldTabViewModelItemClickEventArgs> ItemClick;
    }

    public class EventFieldTabViewModelItemClickEventArgs : EventArgs
    {
        public EventFieldTabViewModelItemClickEventArgs(EventField item)
        {
            Item = item;
        }

        public EventField Item { get; }
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
        public object Value { get; set; }

        public DateTime? DateValue { get => Value as DateTime?; set => Value = value; }
        public int NumberValue { get => int.TryParse(Value as string, out int i) ? i : 0; set => Value = value; }
        public string StringValue { get => (string)Value; set => Value = value; }
    }
}
