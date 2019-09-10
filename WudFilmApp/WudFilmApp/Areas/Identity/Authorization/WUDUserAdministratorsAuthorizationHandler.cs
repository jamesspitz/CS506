using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using WudFilmApp.Areas.Identity.Data;

namespace WudFilmApp.Areas.Identity.Authorization
{
    public class WUDUserAdministratorsAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, WudFilmAppUser>
    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     WudFilmAppUser resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            /*if (context.User.IsInRole(WudFilmAppUser.ContactAdministratorsRole))
            {
                context.Succeed(requirement);
            }*/

            return Task.CompletedTask;
        }
    }
}
