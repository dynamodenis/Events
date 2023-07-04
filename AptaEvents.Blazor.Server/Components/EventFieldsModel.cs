using AptaEvents.Module.BusinessObjects;
using DevExpress.ExpressApp.Blazor.Components.Models;

namespace AptaEvents.Blazor.Server.Components
{
    public class EventFieldsModel : ComponentModelBase
    {
        public IList<EventField> Value
        {
            get => GetPropertyValue<IList<EventField>>();
            set => SetPropertyValue(value);
        }

        public IEnumerable<TabWithEventFieldsViewModel> Tabs
        {
            get => GetPropertyValue<IEnumerable<TabWithEventFieldsViewModel>>();
            set => SetPropertyValue(value);
        }

        public void SetValueFromUI(string value)
        {
            SetPropertyValue(value, notify: false, nameof(Value));
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public void OnItemClick(SimpleEventFieldViewModel item) =>
            ItemClick?.Invoke(this, new EventFieldTabViewModelItemClickEventArgs(item));
        public event EventHandler<EventFieldTabViewModelItemClickEventArgs> ItemClick;
        public event EventHandler ValueChanged;
    }
}
