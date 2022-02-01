using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthPolicyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var role = identity.Claims.Where(x => x.Type == "extension_Role").Select(x => x.Value).FirstOrDefault();
            if (role == "Admin")
            {
                var jsonString = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json"));
                return jsonString;
            }
            else
            {
                return Unauthorized();
            }
           
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
