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
            tournamentNameField.Tab = hiddenTab;
            tournamentNameField.Type = FieldType.String;
        }

        var EventScoringFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Event Scoring Flag");
        if (EventScoringFlag == null)
        {
            EventScoringFlag = ObjectSpace.CreateObject<Field>();
            EventScoringFlag.Name = "Event Scoring Flag";
            EventScoringFlag.Tab = hiddenTab;
            EventScoringFlag.Type = FieldType.String;
        }

        var TournamentScoringFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Tournament Scoring Flag");
        if (TournamentScoringFlag == null)
        {
            TournamentScoringFlag = ObjectSpace.CreateObject<Field>();
            TournamentScoringFlag.Name = "Tournament Scoring Flag";
            TournamentScoringFlag.Tab = hiddenTab;
            TournamentScoringFlag.Type = FieldType.String;
        }

        var ShowWaitingListFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Show Waiting List Flag");
        if (ShowWaitingListFlag == null)
        {
            ShowWaitingListFlag = ObjectSpace.CreateObject<Field>();
            ShowWaitingListFlag.Name = "Show Waiting List Flag";
            ShowWaitingListFlag.Tab = hiddenTab;
            ShowWaitingListFlag.Type = FieldType.String;
        }

        var SeasonName = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Season Name");
        if (SeasonName == null)
        {
            SeasonName = ObjectSpace.CreateObject<Field>();
            SeasonName.Name = "Season Name";
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
        var StartDate = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Start Date");
        if (StartDate == null)
        {
            StartDate = ObjectSpace.CreateObject<Field>();
            StartDate.Name = "Start Date";
            StartDate.Tab = hiddenTab;
            StartDate.Type = FieldType.String;
        }
        var EndDate = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "End Date");
        if (EndDate == null)
        {
            EndDate = ObjectSpace.CreateObject<Field>();
            EndDate.Name = "End Date";
            EndDate.Tab = hiddenTab;
            EndDate.Type = FieldType.String;
        }
        var TournamentType = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Tournament Type");
        if (TournamentType == null)
        {
            TournamentType = ObjectSpace.CreateObject<Field>();
            TournamentType.Name = "Tournament Type";
            TournamentType.Tab = hiddenTab;
        }
        var Capacity = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Capacity");
        if (Capacity == null)
        {
            Capacity = ObjectSpace.CreateObject<Field>();
            Capacity.Name = "Capacity";
            Capacity.Tab = hiddenTab;
            Capacity.Type = FieldType.Number;
        }
        var CancelledFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Cancelled Flag");
        if (CancelledFlag == null)
        {
            CancelledFlag = ObjectSpace.CreateObject<Field>();
            CancelledFlag.Name = "Cancelled Flag";
            CancelledFlag.Tab = hiddenTab;
            CancelledFlag.Type = FieldType.String;
        }
        var EntryOpenFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Entry Open Flag");
        if (EntryOpenFlag == null)
        {
            EntryOpenFlag = ObjectSpace.CreateObject<Field>();
            EntryOpenFlag.Name = "Entry Open Flag";
            EntryOpenFlag.Tab = hiddenTab;
            EntryOpenFlag.Type = FieldType.String;
        }
        var NRTFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "NRT Flag");
        if (NRTFlag == null)
        {
            NRTFlag = ObjectSpace.CreateObject<Field>();
            NRTFlag.Name = "NRT Flag";
            NRTFlag.Tab = hiddenTab;
            NRTFlag.Type = FieldType.String;
        }
        var AptaTourFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Tour Flag");
        if (AptaTourFlag == null)
        {
            AptaTourFlag = ObjectSpace.CreateObject<Field>();
            AptaTourFlag.Name = "Tour Flag";
            AptaTourFlag.Tab = hiddenTab;
            AptaTourFlag.Type = FieldType.String;
        }
        var GrandPrixFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Grand Prix Flag");
        if (GrandPrixFlag == null)
        {
            GrandPrixFlag = ObjectSpace.CreateObject<Field>();
            GrandPrixFlag.Name = "Grand Prix Flag";
            GrandPrixFlag.Tab = hiddenTab;
            GrandPrixFlag.Type = FieldType.String;
        }
        var NationalChampionshipFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "National Championship Flag");
        if (NationalChampionshipFlag == null)
        {
            NationalChampionshipFlag = ObjectSpace.CreateObject<Field>();
            NationalChampionshipFlag.Name = "National Championship Flag";
            NationalChampionshipFlag.Tab = hiddenTab;
            NationalChampionshipFlag.Type = FieldType.String;
        }
        var PTIFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "PTI Flag");
        if (PTIFlag == null)
        {
            PTIFlag = ObjectSpace.CreateObject<Field>();
            PTIFlag.Name = "PTI Flag";
            PTIFlag.Tab = hiddenTab;
            PTIFlag.Type = FieldType.String;
        }
        var JuniorFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Junior Flag");
        if (JuniorFlag == null)
        {
            JuniorFlag = ObjectSpace.CreateObject<Field>();
            JuniorFlag.Name = "Junior Flag";
            JuniorFlag.Tab = hiddenTab;
            JuniorFlag.Type = FieldType.String;
        }
        var MastersFlag = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Masters Flag");
        if (MastersFlag == null)
        {
            MastersFlag = ObjectSpace.CreateObject<Field>();
            MastersFlag.Name = "Masters Flag";
            MastersFlag.Tab = hiddenTab;
            MastersFlag.Type = FieldType.String;
        }
        var EntryOpenDate = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Entry Open Date");
        if (EntryOpenDate == null)
        {
            EntryOpenDate = ObjectSpace.CreateObject<Field>();
            EntryOpenDate.Name = "Entry Open Date";
            EntryOpenDate.Tab = hiddenTab;
            EntryOpenDate.Type = FieldType.String;
        }
        var EntryCloseDate = ObjectSpace.FirstOrDefault<Field>(f => f.Name == "Entry Close Date");
        if (EntryCloseDate == null)
        {
            EntryCloseDate = ObjectSpace.CreateObject<Field>();
            EntryCloseDate.Name = "Entry Close Date";
            EntryCloseDate.Tab = hiddenTab;
            EntryCloseDate.Type = FieldType.String;
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
