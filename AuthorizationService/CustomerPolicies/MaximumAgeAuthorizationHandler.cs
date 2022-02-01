using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AuthorizationService.CustomerPolicies
{
    public class MaximumAgeAuthorizationHandler : AuthorizationHandler<MaximumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaximumAgeRequirement requirement)
        {
            // Bail out if the office number claim isn't present
            if (!context.User.HasClaim(c => c.Type == "Age"))
            {
                return Task.CompletedTask;
            }

            // Bail out if we can't read an int from the 'office' claim
            if (!int.TryParse(context.User.FindFirst(c => c.Type == "Age").Value, out int age))
            {
                return Task.CompletedTask;
            }

            // Finally, validate that the office number from the claim is not greater
            // than the requirement's maximum
            if (age <= requirement.MaximumAgeRequired)
            {
                // Mark the requirement as satisfied
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    //A custom authorization requirement which requires office number to be below a certain value
    public class MaximumAgeRequirement : IAuthorizationRequirement
    {
        public MaximumAgeRequirement(int age)
        {
            MaximumAgeRequired = age;
        }

        public int MaximumAgeRequired { get; private set; }
    }
}
