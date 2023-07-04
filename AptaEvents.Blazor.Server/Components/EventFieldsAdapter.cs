using AptaEvents.Module.BusinessObjects;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.CodeParser;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using Microsoft.AspNetCore.Components;

namespace AptaEvents.Blazor.Server.Components
{
    public class EventFieldsAdapter : ComponentAdapterBase
    {
        private IObjectSpace objectSpace;
        private Module.BusinessObjects.Event _event;

        public EventFieldsAdapter(EventFieldsModel componentModel, IObjectSpace objectSpace, Module.BusinessObjects.Event eventObject)
        {
            this.objectSpace = objectSpace;
            this._event = eventObject;

            ComponentModel = componentModel ?? throw new ArgumentNullException(nameof(componentModel));
            ComponentModel.ValueChanged += ComponentModel_ValueChanged;
            ComponentModel.ItemClick += ComponentModel_ItemClick;
        }

        private void ComponentModel_ItemClick(object sender, EventFieldTabViewModelItemClickEventArgs e)
        {
            var editedField = e.Item;
            var current = ComponentModel.Value.FirstOrDefault(f => f.Field == editedField.Field);

            if (current != null)
                current.Value = editedField.Value;
            else
            {
                var eventField = objectSpace.CreateObject<EventField>();

                eventField.Field = editedField.Field;
                eventField.Value = editedField.Value;
                eventField.Event = _event;

                ComponentModel.Value.Add(eventField);
            }

            RaiseValueChanged();
        }

        public EventFieldsModel ComponentModel { get; }

        public override void SetAllowEdit(bool allowEdit)
        {
        }

        public override object GetValue()
        {
            return ComponentModel.Value;
        }

        public override void SetValue(object value)
        {
            ComponentModel.Value = (IList<EventField>)value;

            var tabs = objectSpace.GetObjects<Tab>().OrderBy(o => o.SortOrder);
            var tabEventFields = new List<TabWithEventFieldsViewModel>();

            foreach (var tab in tabs)
            {
                var tabViewModel = new TabWithEventFieldsViewModel
                {
                    Name = tab.Name,
                    Fields = new List<EventFieldViewModel>()
                };

                foreach (var field in tab.Fields.OrderBy(o => o.SortOrder))
                {
                    var eventViewModel = new EventFieldViewModel
                    {
                        Name = field.Name,
                        Type = field.Type,
                        Tab = tab.Name
                    };

                    var existing = ComponentModel.Value.FirstOrDefault(f => f.Field == field.Name);

                    eventViewModel.Value = existing?.Value;

                    tabViewModel.Fields.Add(eventViewModel);
                }

                tabEventFields.Add(tabViewModel);
            }

            ComponentModel.Tabs = tabEventFields;
        }

        protected override RenderFragment CreateComponent()
        {
            return ComponentModelObserver.Create(ComponentModel, EventFieldsRenderer.Create(ComponentModel));
        }
        private void ComponentModel_ValueChanged(object sender, EventArgs e) => RaiseValueChanged();
        public override void SetAllowNull(bool allowNull) { /* ...*/ }
        public override void SetDisplayFormat(string displayFormat) { /* ...*/ }
        public override void SetEditMask(string editMask) { /* ...*/ }
        public override void SetEditMaskType(EditMaskType editMaskType) { /* ...*/ }
        public override void SetErrorIcon(ImageInfo errorIcon) { /* ...*/ }
        public override void SetErrorMessage(string errorMessage) { /* ...*/ }
        public override void SetIsPassword(bool isPassword) { /* ...*/ }
        public override void SetMaxLength(int maxLength) { /* ...*/ }
        public override void SetNullText(string nullText) { /* ...*/ }
    }
}
