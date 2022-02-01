using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AuthorizationService.ProductPolicies
{
    public class ProductPolicies
    {
        public bool CityJobTitlePolicy(List<string> parameters, AuthorizationHandlerContext context)
        {
            bool result = false;
            string city = parameters[0];
            string jobTitle = parameters[1];
            bool cityClaim = context.User.HasClaim(c => c.Type == "city");
            bool jobTitleClaim = context.User.HasClaim(c => c.Type == "jobTitle");
            if (!cityClaim || !jobTitleClaim)
            {
                return false;
            }
            if ((context.User.FindFirst(c => c.Type == "jobTitle").Value.ToLower() == jobTitle.ToLower()) &&
                (context.User.FindFirst(c => c.Type == "city").Value.ToLower() == city.ToLower()))
            {
                return true;
            }
            return result;
        }

        public bool CountryCityPolicy(List<string> parameters, AuthorizationHandlerContext context)
        {
            bool result = false;
            string country = parameters[0];
            string city = parameters[1];
            bool countryClaim = context.User.HasClaim(c => c.Type.ToLower().Equals("country"));
            bool cityClaim = context.User.HasClaim(c => c.Type.ToLower().Equals("city"));
            if (!countryClaim && !cityClaim)
            {
                return false;
            }

            if ((context.User.FindFirst(c => c.Type == "country").Value.ToLower() == country.ToLower()) &&
                (context.User.FindFirst(c => c.Type == "city").Value.ToLower() == city.ToLower()))
            {
                return true;
            }
            return result;
        }

        public bool GenericPolicyValidator(List<ClaimModel> parameters, AuthorizationHandlerContext context)
        {
            bool result = false;
            foreach (var polParams in parameters)
            {
                bool claim1 = context.User.HasClaim(c => c.Type.ToLower().Equals(polParams.ParameterName.ToLower()));
                if (claim1)
                {
                    //Has claim
                    var claimValue = context.User.FindFirst(c => c.Type.ToLower() == polParams.ParameterName.ToLower()).Value;
                    switch (polParams.Operator)
                    {
                        case "=":
                            result = (claimValue.ToLower().Equals(polParams.ParameterValue.ToLower()));
                            break;
                        case ">":
                            result = Convert.ToInt32(claimValue) > Convert.ToInt32(polParams.ParameterValue);
                            break;
                        case "<":
                            result = Convert.ToInt32(claimValue) < Convert.ToInt32(polParams.ParameterValue);
                            break;
                        case "<=":
                            result = Convert.ToInt32(claimValue) <= Convert.ToInt32(polParams.ParameterValue);
                            break;
                        case ">=":
                            result = Convert.ToInt32(claimValue) >= Convert.ToInt32(polParams.ParameterValue);
                            break;
                        case "!=":
                            result = claimValue.ToLower() != polParams.ParameterValue.ToLower();
                            break;
                    }
                }
                else
                {
                    //Has no claim
                    return false;
                }
                if (!result)
                {
                    return false;
                }
            }
            return result;
        }
    }
}
