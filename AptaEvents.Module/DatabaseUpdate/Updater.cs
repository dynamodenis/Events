using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using AptaEvents.Module.BusinessObjects;

namespace AptaEvents.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
#if !RELEASE
            ApplicationUser sampleUser = ObjectSpace.FirstOrDefault<ApplicationUser>(u => u.UserName == "User");
            if (sampleUser == null)
            {
                sampleUser = ObjectSpace.CreateObject<ApplicationUser>();
                sampleUser.UserName = "User";
                // Set a password if the standard authentication type is used
                sampleUser.SetPassword("");

                // The UserLoginInfo object requires a user object Id (Oid).
                // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                ObjectSpace.CommitChanges(); //This line persists created object(s).
                ((ISecurityUserWithLoginInfo)sampleUser).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(sampleUser));
            }
            PermissionPolicyRole defaultRole = CreateDefaultRole();
            sampleUser.Roles.Add(defaultRole);

#endif
            ApplicationUser userAdmin = ObjectSpace.FirstOrDefault<ApplicationUser>(u => u.UserName == "Admin");
            if (userAdmin == null)
            {
                userAdmin = ObjectSpace.CreateObject<ApplicationUser>();
                userAdmin.UserName = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");

                // The UserLoginInfo object requires a user object Id (Oid).
                // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                ObjectSpace.CommitChanges(); //This line persists created object(s).
                ((ISecurityUserWithLoginInfo)userAdmin).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin));
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            PermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Administrator");
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrator";
            }
            adminRole.IsAdministrative = true;
            userAdmin.Roles.Add(adminRole);

            var defaultTab = ObjectSpace.FirstOrDefault<Tab>(t => t.Name == "Default");

            if (defaultTab == null)
            {
                defaultTab = ObjectSpace.CreateObject<Tab>();

                defaultTab.Name = "Default";
                defaultTab.SortOrder = 1;
            }

            var hiddenTab = ObjectSpace.FirstOrDefault<Tab>(t => t.Name == "Hidden");

            if (hiddenTab == null)
            {
                hiddenTab = ObjectSpace.CreateObject<Tab>();

                hiddenTab.Name = "Hidden";
                defaultTab.SortOrder = 0;
            }

        // Create fields Automatically Under the Hidden Tab
        var tournamentNameField = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Tournament Name");
        if (tournamentNameField == null)
        {
            tournamentNameField = ObjectSpace.CreateObject<Field>();
            tournamentNameField.Name = "Tournament Name";
            tournamentNameField.DisplayName = "Tournament Name";
				tournamentNameField.Tab = hiddenTab;
            tournamentNameField.Type = FieldType.String;
        }

        var EventScoringFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "EventScoringFlag");
        if (EventScoringFlag == null)
        {
            EventScoringFlag = ObjectSpace.CreateObject<Field>();
            EventScoringFlag.Name = "EventScoringFlag";
            EventScoringFlag.DisplayName = "Event Scoring Flag";
				EventScoringFlag.Tab = hiddenTab;
            EventScoringFlag.Type = FieldType.Boolean;
        }

        var TournamentScoringFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "TournamentScoringFlag");
        if (TournamentScoringFlag == null)
        {
            TournamentScoringFlag = ObjectSpace.CreateObject<Field>();
            TournamentScoringFlag.Name = "TournamentScoringFlag";
            TournamentScoringFlag.DisplayName = "Tournament Scoring Flag";
				TournamentScoringFlag.Tab = hiddenTab;
            TournamentScoringFlag.Type = FieldType.Boolean;
        }

        var ShowWaitingListFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "ShowWaitingListFlag");
        if (ShowWaitingListFlag == null)
        {
            ShowWaitingListFlag = ObjectSpace.CreateObject<Field>();
            ShowWaitingListFlag.Name = "ShowWaitingListFlag";
            ShowWaitingListFlag.DisplayName = "Show Waiting List Flag";
				ShowWaitingListFlag.Tab = hiddenTab;
            ShowWaitingListFlag.Type = FieldType.Boolean;
        }

        var SeasonName = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "SeasonName");
        if (SeasonName == null)
        {
            SeasonName = ObjectSpace.CreateObject<Field>();
            SeasonName.Name = "SeasonName";
            SeasonName.DisplayName = "Season Name";
				SeasonName.Tab = hiddenTab;
            SeasonName.Type = FieldType.String;
        }

        var Region = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Region");
        if (Region == null)
        {
            Region = ObjectSpace.CreateObject<Field>();
            Region.Name = "Region";
            Region.Tab = hiddenTab;
            Region.Type = FieldType.Number;
        }
        var StartDate = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "StartDate");
        if (StartDate == null)
        {
            StartDate = ObjectSpace.CreateObject<Field>();
            StartDate.Name = "StartDate";
            StartDate.DisplayName = "Start Date";
				StartDate.Tab = hiddenTab;
            StartDate.Type = FieldType.Date;
        }
        var EndDate = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "EndDate");
        if (EndDate == null)
        {
            EndDate = ObjectSpace.CreateObject<Field>();
            EndDate.Name = "EndDate";
            EndDate.DisplayName = "End Date";
            EndDate.Tab = hiddenTab;
            EndDate.Type = FieldType.Date;
        }
        var TournamentType = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "TournamentType");
        if (TournamentType == null)
        {
            TournamentType = ObjectSpace.CreateObject<Field>();
            TournamentType.Name = "TournamentType";
            TournamentType.DisplayName = "Tournament Type";
            TournamentType.Tab = hiddenTab;
			TournamentType.Type = FieldType.String;
		}
		var Capacity = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Capacity");
        if (Capacity == null)
        {
            Capacity = ObjectSpace.CreateObject<Field>();
            Capacity.Name = "Capacity";
            Capacity.Tab = hiddenTab;
            Capacity.Type = FieldType.Number;
        }
        var CancelledFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "CancelledFlag");
        if (CancelledFlag == null)
        {
            CancelledFlag = ObjectSpace.CreateObject<Field>();
            CancelledFlag.Name = "CancelledFlag";
            CancelledFlag.DisplayName = "Cancelled";
            CancelledFlag.Tab = hiddenTab;
            CancelledFlag.Type = FieldType.Boolean;
        }
        var EntryOpenFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "EntryOpenFlag");
        if (EntryOpenFlag == null)
        {
            EntryOpenFlag = ObjectSpace.CreateObject<Field>();
            EntryOpenFlag.Name = "EntryOpenFlag";
            EntryOpenFlag.DisplayName = "Entry Open Flag";
            EntryOpenFlag.Tab = hiddenTab;
            EntryOpenFlag.Type = FieldType.Boolean;
        }
        var NRTFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "NRTFlag");
        if (NRTFlag == null)
        {
            NRTFlag = ObjectSpace.CreateObject<Field>();
            NRTFlag.Name = "NRTFlag";
            NRTFlag.DisplayName = "NRT Flag";
				NRTFlag.Tab = hiddenTab;
            NRTFlag.Type = FieldType.Boolean;
        }
        var AptaTourFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "AptaTourFlag");
        if (AptaTourFlag == null)
        {
            AptaTourFlag = ObjectSpace.CreateObject<Field>();
            AptaTourFlag.Name = "AptaTourFlag";
            AptaTourFlag.DisplayName = "Tour Flag";
            AptaTourFlag.Tab = hiddenTab;
            AptaTourFlag.Type = FieldType.Boolean;
        }
        var GrandPrixFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "GrandPrixFlag");
        if (GrandPrixFlag == null)
        {
            GrandPrixFlag = ObjectSpace.CreateObject<Field>();
            GrandPrixFlag.Name = "GrandPrixFlag";
            GrandPrixFlag.DisplayName = "Grand Prix Flag";
				GrandPrixFlag.Tab = hiddenTab;
            GrandPrixFlag.Type = FieldType.Boolean;
        }
        var NationalChampionshipFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "NationalChampionshipFlag");
        if (NationalChampionshipFlag == null)
        {
            NationalChampionshipFlag = ObjectSpace.CreateObject<Field>();
            NationalChampionshipFlag.Name = "NationalChampionshipFlag";
            NationalChampionshipFlag.DisplayName = "National Championship Flag";
				NationalChampionshipFlag.Tab = hiddenTab;
            NationalChampionshipFlag.Type = FieldType.Boolean;
        }
        var PTIFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "PTIFlag");
        if (PTIFlag == null)
        {
            PTIFlag = ObjectSpace.CreateObject<Field>();
            PTIFlag.Name = "PTIFlag";
            PTIFlag.DisplayName = "PTI Flag";
				PTIFlag.Tab = hiddenTab;
            PTIFlag.Type = FieldType.Boolean;
        }
        var JuniorFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "JuniorFlag");
        if (JuniorFlag == null)
        {
            JuniorFlag = ObjectSpace.CreateObject<Field>();
            JuniorFlag.Name = "JuniorFlag";
            JuniorFlag.DisplayName = "Junior Flag";
            JuniorFlag.Tab = hiddenTab;
            JuniorFlag.Type = FieldType.Boolean;
        }
        var MastersFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "MastersFlag");
        if (MastersFlag == null)
        {
            MastersFlag = ObjectSpace.CreateObject<Field>();
            MastersFlag.Name = "MastersFlag";
            MastersFlag.DisplayName = "Masters Flag";
            MastersFlag.Tab = hiddenTab;
            MastersFlag.Type = FieldType.Boolean;
        }
        var EntryOpenDate = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "EntryOpenDate");
        if (EntryOpenDate == null)
        {
            EntryOpenDate = ObjectSpace.CreateObject<Field>();
            EntryOpenDate.Name = "EntryOpenDate";
            EntryOpenDate.DisplayName = "Entry Open Date";
            EntryOpenDate.Tab = hiddenTab;
            EntryOpenDate.Type = FieldType.String;
        }
        var EntryCloseDate = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "EntryCloseDate");
        if (EntryCloseDate == null)
        {
            EntryCloseDate = ObjectSpace.CreateObject<Field>();
            EntryCloseDate.Name = "EntryCloseDate";
            EntryCloseDate.DisplayName = "Entry Close Date";
            EntryCloseDate.Tab = hiddenTab;
            EntryCloseDate.Type = FieldType.Date;
        }
        ObjectSpace.CommitChanges();
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
    private PermissionPolicyRole CreateDefaultRole() {
        PermissionPolicyRole defaultRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default");
        if(defaultRole == null) {
            defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            defaultRole.Name = "Default";

                defaultRole.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                 //xaf-roles
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Events/Items/Event_ListView", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "StoredPassword", cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
            }
            return defaultRole;
        }


    }
}
