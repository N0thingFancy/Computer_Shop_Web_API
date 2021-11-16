using Kompiuteriu_Web.Auth.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Auth
{
    public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirement, IUserOwnedResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement, IUserOwnedResource resource)
        {
            if (context.User.IsInRole(UserRoles.Admin) || context.User.FindFirst(CustomClaims.UserId).Value == resource.User_id_saved)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
    public record SameUserRequirement : IAuthorizationRequirement;
}
