using AuthorizationService;
using CustomersAPIServices.DataStore;
using CustomersAPIServices.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomersAPIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        //[Authorize(Policy = "AgeUnder25")]
        [Authorize(Policy = "AgeAbove25RoleAdmin")]
        public IEnumerable<Model.CustomerModel> Get()
        {
            return CustomerData.list;
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            foreach (var x in CustomerData.list)
            {
                if (x.Id == id)
                {
                    return x.Name;
                }
            }
            return "Not Found";
        }
    }
}
