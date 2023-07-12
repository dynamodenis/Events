using AptaEvents.Module.BusinessObjects;
using AptaEvents.Module.Services;
using Castle.Core.Resource;
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
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AptaEvents.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Event_DetailViewController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public Event_DetailViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Event);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Debug.WriteLine("Activated");
            // Perform various tasks depending on the target View.
            
        }
        protected override async void OnViewControlsCreated()
        {
            ///base.OnViewControlsCreated();
            // Access and customize the target View control.
            if (View.CurrentObject == null)
                return;

            var eventObject = (Event)View.CurrentObject;
            Debug.WriteLine("New item " + eventObject.Name);
            //await EventsServices.GetEvents(eventObject.Date, eventObject.Name  = "Fake Big Event");
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
