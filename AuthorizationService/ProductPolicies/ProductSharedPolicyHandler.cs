using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationService.ProductPolicies
{
    public class ProductSharedPolicyHandler : AuthorizationHandler<ProductSharedPolicy>
    {
        private readonly IHttpContextAccessor contextAccessor;
        public ProductSharedPolicyHandler(IHttpContextAccessor contextAccessor)
        {

            this.contextAccessor = contextAccessor;

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProductSharedPolicy policies)
        {
            //var configurationBuilder = new ConfigurationBuilder();
            //var path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json");
            //configurationBuilder.AddJsonFile(path, false, reloadOnChange: true);

            //var root = configurationBuilder.Build();

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };


            //new implementation for generic policies
            var httpContext = contextAccessor.HttpContext;
            var requestUrl = httpContext.Request.Path;
            var reuqestHttpVerb = httpContext.Request.Method;

            string idToken = httpContext.Request.Headers["Authorization"];
            client.DefaultRequestHeaders.Add("Authorization", idToken);
            HttpResponseMessage response = client.GetAsync($"/api/values").GetAwaiter().GetResult();

            var jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var rootPolicyModel = JsonConvert.DeserializeObject<List<Root>>(jsonString);

            bool isUrlMatched = false;
            List<Policy> requestPolicies = new List<Policy>();
            string policyOperator = "AND";
            foreach (var rootPolicy in rootPolicyModel)
            {
                //Matching Json Url with request url
                isUrlMatched = Match(rootPolicy.Url, requestUrl, rootPolicy.Method, reuqestHttpVerb);
                if (isUrlMatched)
                {
                    requestPolicies = rootPolicy.Policies ?? new List<Policy>();
                    policyOperator = rootPolicy.PolicyOperator;
                    break;
                }
            }
            if (!isUrlMatched)
            {
                return Task.CompletedTask;
            }

            ProductPolicies productPolicies = new ProductPolicies();
            bool policyResult = false;
            foreach (Policy policy in requestPolicies)
            {
                //switch (policy.Name)
                //{
                //    case "CityJobTitlePolicy":
                //        policyResult = productPolicies.CityJobTitlePolicy(policy.Parameters, context);
                //        break;

                //    case "CountryCityPolicy":
                //        policyResult = productPolicies.CountryCityPolicy(policy.Parameters, context);
                //        break;
                //}

                policyResult = productPolicies.GenericPolicyValidator(policy.Parameters, context);
                if (requestPolicies.Count > 1)
                {
                    if (policyOperator.ToUpper() == "OR")
                    {
                        if (policyResult)
                        {
                            break;
                        }
                    }
                    if (policyOperator.ToUpper() == "AND")
                    {
                        if (!policyResult)
                        {
                            break;
                        }
                    }
                }
            }
            if (policyResult || !requestPolicies.Any())
            {
                context.Succeed(policies);
            }
            return Task.CompletedTask;
        }

        public bool Match(string routeTemplate, string requestPath, string templateMethod, string requestMethod)
        {
            var template = TemplateParser.Parse(routeTemplate);
            RouteValueDictionary valuePairs = new RouteValueDictionary();
            var matcher = new TemplateMatcher(template, GetDefaults(template));
            var isMatched = matcher.TryMatch(requestPath, valuePairs);
            if (isMatched)
            {
                return templateMethod.ToLower().Equals(requestMethod.ToLower()) ? true : false;
            }
            return false;
        }

        // This method extracts the default argument values from the template.
        private RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
        {
            var result = new RouteValueDictionary();
            foreach (var parameter in parsedTemplate.Parameters)
            {
                if (parameter.DefaultValue != null)
                {
                    result.Add(parameter.Name, parameter.DefaultValue);
                }
            }
            return result;
        }
    }

    public class ProductSharedPolicy : IAuthorizationRequirement
    {
        public ProductSharedPolicy()
        {

            //Read policies from config


            //_connectionString = root.GetSection("ConnectionString").GetSection("DataConnection").Value;
            //var appSetting = root.GetSection("ApplicationSettings");
            //var jsonString = root.GetSection("ConfigPolicies");
            //  AllPolicies = JsonConvert.DeserializeObject<List<PolicyViewModel>>(jsonString);
            //var jsonString = File.ReadAllText(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json"));
            // AllPolicies = JsonConvert.DeserializeObject<List<PolicyViewModel>>(jsonString);

        }
        public List<PolicyViewModel> AllPolicies { get; private set; }
    }
}
