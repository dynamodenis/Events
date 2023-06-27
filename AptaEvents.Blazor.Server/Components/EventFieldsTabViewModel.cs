using AptaEvents.Module.BusinessObjects;
using DevExpress.ExpressApp.Blazor.Components.Models;

namespace AptaEvents.Blazor.Server.Components
{
    public class EventFieldsTabViewModel  : ComponentModelBase
    {
        public IEnumerable<EventField> Data
        {
            get => GetPropertyValue<IEnumerable<EventField>>();
            set => SetPropertyValue(value);
        }

        public IEnumerable<Tab> Tabs
        {
            get => GetPropertyValue<IEnumerable<Tab>>();
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
}
