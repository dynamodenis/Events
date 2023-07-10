using System;
using System.Security.Claims;
using System.Security.Principal;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using AptaEvents.Module.BusinessObjects;
using IdentityModel;

namespace AptaEvents.Blazor.Server.Services {

    public class CustomAuthenticationProvider : IAuthenticationProviderV2
    {
        private readonly IPrincipalProvider principalProvider;

        public CustomAuthenticationProvider(IPrincipalProvider principalProvider)
        {
            this.principalProvider = principalProvider;
        }

        public object Authenticate(IObjectSpace objectSpace)
        {
            if (!CanHandlePrincipal(principalProvider.User))
            {
                return null;
            }

            ClaimsPrincipal claimsPrincipal = (ClaimsPrincipal)principalProvider.User;
            var userIdClaim = claimsPrincipal.FindFirst("sub") ?? claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Unknown user id");

            var providerUserKey = userIdClaim.Value;
            var loginProviderName = claimsPrincipal.Identity.AuthenticationType;
            //var userName = claimsPrincipal.Identity.Name;
            Claim userName = claimsPrincipal.FindFirst(JwtClaimTypes.Name);

            // Find the first claim with the claim type "role" and value starting with "AptaEvents"
            Claim userRole = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Role && c.Value.ToLower() == "aptaevents");

            if (userRole != null)
            {
                var userLoginInfo = FindUserLoginInfo(objectSpace, loginProviderName, providerUserKey);
                if (userLoginInfo != null)
                {
                    return userLoginInfo.User;
                }

                // If the user is not found, you can create a new user account or perform additional checks/validation.

                // Example: Create a new user account
                var user = objectSpace.CreateObject<ApplicationUser>();
                user.UserName = userName.Value;
                user.SetPassword(Guid.NewGuid().ToString());

                // Add roles to the user
                user.Roles.Add(objectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "User"));

                ((ISecurityUserWithLoginInfo)user).CreateUserLoginInfo(loginProviderName, providerUserKey);
                objectSpace.CommitChanges();

                return user;
            }
            else
            {
                // Handle the case when the desired claim does not exist
                // For example, throw an exception or return a default result
                throw new Exception("Desired claim does not exist.");
            }
        }

        private bool CanHandlePrincipal(IPrincipal user)
        {
            // Add your custom conditions to determine if the user can be handled by this authentication provider.
            // For example, you can check the user's authentication type or any other custom criteria.
            return user.Identity.IsAuthenticated &&
                   user.Identity.AuthenticationType != SecurityDefaults.Issuer &&
                   user.Identity.AuthenticationType != SecurityDefaults.PasswordAuthentication &&
                   user.Identity.AuthenticationType != SecurityDefaults.WindowsAuthentication &&
                   !(user is WindowsPrincipal);
        }

        private ISecurityUserLoginInfo FindUserLoginInfo(IObjectSpace objectSpace, string loginProviderName, string providerUserKey)
        {
            return objectSpace.FirstOrDefault<ApplicationUserLoginInfo>(userLoginInfo =>
                                userLoginInfo.LoginProviderName == loginProviderName &&
                                userLoginInfo.ProviderUserKey == providerUserKey);
        }
    }

}
