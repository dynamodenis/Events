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
            SimpleAction clearTasksAction = new SimpleAction(this, "ClearTaskAction", PredefinedCategory.View)
            {
                //Specify the Action's button caption.
                Caption = "Publish Data",
                //Specify the confirmation message that pops up when a user executes an Action.
                ConfirmationMessage = "Are you sure you want to clear the Tasks list?",
                //Specify the icon of the Action's button in the interface.
                ImageName = "Action_Clear"
            };
            //This event fires when a user clicks the Simple Action control. Handle this event to execute custom code.
            clearTasksAction.Execute += PublishDataAction_Execute;
        }
        private void PublishDataAction_Execute(Object sender, SimpleActionExecuteEventArgs e)
        {
            var item = ((Event)View.CurrentObject).EventFields.Select(item => new PublishData()
            {
                Tab = new TabDto
                {
                    Name = item.Tab.Name,
                    SortOrder = item.Tab.SortOrder,
                    Fields = item.Tab.Fields.Select(field => new FieldDto
                    {
                        Name = field.Name,
                        SortOrder = field.SortOrder,
                        Type = field.Type.ToString(),
                        Value = item.Value
                    }).ToList()
                }
            });

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string serializedData = JsonSerializer.Serialize(item, jsonOptions);

            // Set the value of the PublishData property
            ((Event)View.CurrentObject).PublishData = serializedData;

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
