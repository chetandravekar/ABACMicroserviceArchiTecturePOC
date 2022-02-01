using DotNetCoreAPIMicroservice_POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroservice.DataStore
{
    public static class ProductData
    {
        public static List<Product> products = new List<Product>
        {
            new Product{Id = 1, Name = "MilkShake", Price = 50},
            new Product{Id = 2, Name = "Tornado", Price = 300},
            new Product{Id = 3, Name = "Burger", Price = 150},
            new Product{Id = 4, Name = "Sandwich", Price = 200},
            new Product{Id = 5, Name = "Pizza", Price = 400}
        };
    }
}
