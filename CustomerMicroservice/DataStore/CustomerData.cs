using CustomersAPIServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersAPIServices.DataStore
{
    public static class CustomerData
    {
        public static IEnumerable<CustomerModel> list = new List<CustomerModel>
        {
            new CustomerModel()
            {
                Id = 1,
                Name = "Sourabh",
                Address = "Nagpur"
            },
            new CustomerModel()
            {
                Id = 2,
                Name = "Shaili",
                Address = "Mumbai"
            },
            new CustomerModel()
            {
                Id = 3,
                Name = "Saloni",
                Address = "Pune"
            }
        };
    }
}
