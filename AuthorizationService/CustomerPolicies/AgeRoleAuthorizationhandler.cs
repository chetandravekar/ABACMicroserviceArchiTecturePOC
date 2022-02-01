using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationService.CustomerPolicies
{
    public class AgeRoleAuthorizationhandler : AuthorizationHandler<MaximumAgeRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaximumAgeRoleRequirement requirement)
        {
            //checked whether token has Age and Role claims
            if (!context.User.HasClaim(c => c.Type == "Age"))
            {
                return Task.CompletedTask;
            }

            //get values of age and try to convert it in integer
            if (!int.TryParse(context.User.FindFirst(c => c.Type == "Age").Value, out int age))
            {
                return Task.CompletedTask;
            }

            if (age > requirement.MaximumAgeRequired && context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value == Role.Admin)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    //A custom authorization requirement which requires office number to be below a certain value
    public class MaximumAgeRoleRequirement : IAuthorizationRequirement
    {
        public MaximumAgeRoleRequirement(int age, string role)
        {
            MaximumAgeRequired = age;
            RoleRequired = role;
        }
        public int MaximumAgeRequired { get; private set; }
        public string RoleRequired { get; private set; }
    }
}
