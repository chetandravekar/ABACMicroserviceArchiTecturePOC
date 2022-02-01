using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AuthorizationService.ProductPolicies
{
    public class DesignationAgeAuthorizationHandler : AuthorizationHandler<MaximumAgeRoleDesignationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaximumAgeRoleDesignationRequirement requirement)
        {
            //checked whether token has Age and Role claims
            if (!context.User.HasClaim(c => c.Type == "Age" && c.Type == "Designation"))
            {
                return Task.CompletedTask;
            }

            //get values of age and try to convert it in integer
            if (!int.TryParse(context.User.FindFirst(c => c.Type == "Age").Value, out int age))
            {
                return Task.CompletedTask;
            }

            if (age > requirement.MaximumAgeRequired && context.User.FindFirst(c => c.Type == "Designation").Value == DesignationEnum.Consultant)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    //A custom authorization requirement which requires office number to be below a certain value
    public class MaximumAgeRoleDesignationRequirement : IAuthorizationRequirement
    {
        public MaximumAgeRoleDesignationRequirement(int age, string designation)
        {
            MaximumAgeRequired = age;
            DesignationRequired = designation;
        }
        public int MaximumAgeRequired { get; private set; }
        public string DesignationRequired { get; private set; }
    }
}
