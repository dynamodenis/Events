using AptaEvents.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Components;
using System.Collections;
using System.ComponentModel;

namespace AptaEvents.Blazor.Server.Components
{
    [ListEditor(typeof(EventField))]
    public class BlazorCustomEventViewListEditor : ListEditor, IComplexListEditor
    {
        private EventField[] selectedObjects = Array.Empty<EventField>();

        private CollectionSourceBase collectionSource;
        private XafApplication application;

        public void Setup(CollectionSourceBase collectionSource,
            XafApplication application)
        {
            this.collectionSource = collectionSource;
            this.application = application;
        }

        public BlazorCustomEventViewListEditor(IModelListView model) : base(model) { }

        public override SelectionType SelectionType => SelectionType.Full;

        public override IList GetSelectedObjects() => selectedObjects;

        public override void Refresh()
        {
            if (Control is EventFieldsViewListViewHolder holder)
                holder.ComponentModel.Refresh();
        }

        public override void BreakLinksToControls()
        {
            if (Control is EventFieldsViewListViewHolder holder)
                holder.ComponentModel.ItemClick -= ComponentModel_ItemClick;

            AssignDataSourceToControl(null);

            base.BreakLinksToControls();
        }

        protected override object CreateControlsCore() =>
            new EventFieldsViewListViewHolder(new EventFieldsTabViewModel());

        protected override void AssignDataSourceToControl(object dataSource)
        {
            if (Control is EventFieldsViewListViewHolder holder)
            {
                if (holder.ComponentModel.Data is IBindingList bindingList)
                    bindingList.ListChanged -= BindingList_ListChanged;

                if (dataSource is IBindingList newBindingList)
                    newBindingList.ListChanged += BindingList_ListChanged;

                holder.ComponentModel.CollectionSource = collectionSource;

                var sourceList = collectionSource.GetEnumerable<EventField>().ToList();
                holder.ComponentModel.Data = sourceList;

                var tabs = collectionSource.ObjectSpace.GetObjects<Tab>();
                var tabEventFields = new List<TabWithEventFieldsViewModel>();

                foreach (var tab in tabs)
                {
                    var tabViewModel = new TabWithEventFieldsViewModel
                    {
                        Name = tab.Name,
                        Fields = new List<EventFieldViewModel>()
                    };

                    foreach (var field in tab.Fields)
                    {
                        var eventViewModel = new EventFieldViewModel
                        {
                            Name = field.Name,
                            Type = field.Type,
                            Tab = tab.Name
                        };

                        var existing = sourceList.FirstOrDefault(f => f.Field == field.Name);

                        eventViewModel.Value = existing?.Value;

                        tabViewModel.Fields.Add(eventViewModel);
                    }

                    tabEventFields.Add(tabViewModel);
                }

                holder.ComponentModel.Tabs = tabEventFields;
            }
        }

        protected override void OnControlsCreated()
        {
            if (Control is EventFieldsViewListViewHolder holder)
                holder.ComponentModel.ItemClick += ComponentModel_ItemClick;

            base.OnControlsCreated();
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            Refresh();
        }

        private void ComponentModel_ItemClick(object sender,
                                            EventFieldTabViewModelItemClickEventArgs e)
        {
            selectedObjects = new EventField[] { e.Item };

            OnSelectionChanged();
            OnProcessSelectedItem();
        }

        public class EventFieldsViewListViewHolder : IComponentContentHolder
        {
            private RenderFragment componentContent;

            public EventFieldsViewListViewHolder(EventFieldsTabViewModel componentModel)
            {
                ComponentModel =
                    componentModel ?? throw new ArgumentNullException(nameof(componentModel));
            }

            private RenderFragment CreateComponent() =>
                ComponentModelObserver.Create(ComponentModel,
                                                EventFieldsTabViewRenderer.Create(ComponentModel));

            public EventFieldsTabViewModel ComponentModel { get; }

            RenderFragment IComponentContentHolder.ComponentContent =>
                componentContent ??= CreateComponent();
        }
    }
}
