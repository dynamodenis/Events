using AptaEvents.Module.BusinessObjects;
using AptaEvents.Module.DTO;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;

namespace AptaEvents.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PublishDataController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public PublishDataController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            //Activate the Controller only in the Detail View
            TargetViewType = ViewType.DetailView;
            //Specify the type of objects that can use the Controller
            TargetObjectType = typeof(Event);
            SimpleAction publishAction = new SimpleAction(this, "PublishAction", PredefinedCategory.View)
            {
                //Specify the Action's button caption.
                Caption = "Publish Data",
                //Specify the confirmation message that pops up when a user executes an Action.
                ConfirmationMessage = "Are you sure you want to publish this event's data?",
                //Specify the icon of the Action's button in the interface.
                ImageName = "BO_Transition"
            };
            //This event fires when a user clicks the Simple Action control. Handle this event to execute custom code.
            publishAction.Execute += PublishDataAction_Execute;
        }

        private void PublishDataAction_Execute(Object sender, SimpleActionExecuteEventArgs e)
        {
            // get all tabs in system
            var tabs = ObjectSpace.GetObjects<Tab>().OrderBy(o => o.SortOrder);
            var tabEventFields = new List<TabDto>();

            foreach (var tab in tabs)
            {
                var tabViewModel = new TabDto
                {
                    Name = tab.Name,
                    SortOrder = tab.SortOrder,
                    Fields = new List<FieldDto>()
                };

                foreach (var field in tab.Fields.OrderBy(o => o.SortOrder))
                {
                    // find event field matching this field
                    var eventField = ((Event)View.CurrentObject).EventFields.FirstOrDefault(f => f.Field == field.Name);
                    var value = eventField?.Value;

                    // do not include fields without a value
                    if (string.IsNullOrEmpty(value))
                        continue;

                    var eventViewModel = new FieldDto
                    {
                        Name = field.Name,
                        SortOrder = field.SortOrder,
                        Type = field.Type.ToString(),
                        Expiry = eventField.Expiry,
                        Value = value
                    };

                    tabViewModel.Fields.Add(eventViewModel);
                }

                tabEventFields.Add(tabViewModel);
            }

            // Set the value of the PublishData property
            ((Event)View.CurrentObject).PublishData = new PublishData { Tabs = tabEventFields };

            // Mark the current object as modified
            View.ObjectSpace.SetModified(View.CurrentObject);

            // Commit the changes to persist the modified object
            View.ObjectSpace.CommitChanges();

            // Refresh the view to display the updated PublishData field
            View.Refresh();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
