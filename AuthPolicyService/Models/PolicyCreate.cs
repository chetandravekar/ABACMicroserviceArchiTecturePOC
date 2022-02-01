using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthPolicyService.Models
{
    public class PolicyCreate
    {
        public string Url { get; set; }
        public string PolicyOperator { get; set; }
        public string PolicyInfo { get; set; }
        public string Method { get; set; }
        public List<ClaimModel> Policies { get; set; }
    }

    //public class ClaimModel
    //{
    //    public string ParameterName { get; set; }
    //    public string ParameterValue { get; set; }
    //    public string Operator { get; set; }
    //}
}
