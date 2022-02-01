using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationService
{
    public class PolicyViewModel
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public List<string> Parameters { get; set; }
    }
}
