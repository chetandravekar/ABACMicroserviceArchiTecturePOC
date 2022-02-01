using AuthorizationService;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AuthorizationService.ProductPolicies
{
    public class DesignationAuthorizationHandler : AuthorizationHandler<DesignationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DesignationRequirement requirement)
        {
            //checked whether token has Age and Role claims
            if (!context.User.HasClaim(c => c.Type == "Designation"))
            {
                return Task.CompletedTask;
            }

            if (context.User.FindFirst(c => c.Type == "Designation").Value == DesignationEnum.SrLead)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    //A custom authorization requirement which requires office number to be below a certain value
    public class DesignationRequirement : IAuthorizationRequirement
    {
        public DesignationRequirement(string designation)
        {
            DesignationRequired = designation;
        }
        public string DesignationRequired { get; private set; }
    }
}
