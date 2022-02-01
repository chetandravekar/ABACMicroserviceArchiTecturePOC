using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationService
{
    public class PolicyViewModel
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public string PolicyOperator { get; set; }
        public List<Policy> Policies { get; set; }
    }

 
    public class Policy
    {
        public string Name { get; set; }
        public List<ClaimModel> Parameters { get; set; }
    }

    public class Root
    {
        public string SrNo { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string PolicyInfo { get; set; }
        public string PolicyOperator { get; set; }
        public List<Policy> Policies { get; set; }
    }

    public class ClaimModel
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string Operator { get; set; }
    }
}
