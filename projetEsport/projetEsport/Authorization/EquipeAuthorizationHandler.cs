using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Authorization
{
    public class EquipeAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Equipe>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Equipe resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name == ESportOperations.Create.Name
                    && (context.User.IsInRole(Constants.AdministrateursRole) || context.User.IsInRole(Constants.OrganisteursRole)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
