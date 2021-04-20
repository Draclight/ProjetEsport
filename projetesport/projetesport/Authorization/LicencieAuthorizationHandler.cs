using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using projetesport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetesport.Authorization
{
    public class LicencieAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Licencie>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OperationAuthorizationRequirement requirement,
                                                       Licencie resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // Managers can approve or reject.
            if (context.User.IsInRole(Constants.AdministrateursRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
