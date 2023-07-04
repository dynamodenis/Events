using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using AptaEvents.Module.BusinessObjects;
using DevExpress.ExpressApp;

namespace AptaEvents.Blazor.Server.Components
{
    [PropertyEditor(typeof(IList<EventField>), true)]
    public class EventFieldsPropertyEditor : BlazorPropertyEditorBase, IComplexViewItem
    {
        private IObjectSpace objectSpace;
        private XafApplication application;

        public EventFieldsPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            this.objectSpace = objectSpace;
            this.application = application;
        }

        protected override IComponentAdapter CreateComponentAdapter() => new EventFieldsAdapter(new EventFieldsModel(), objectSpace, View.CurrentObject as Event);
    }
}
