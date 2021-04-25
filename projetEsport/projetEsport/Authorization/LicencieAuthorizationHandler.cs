using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Authorization
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
