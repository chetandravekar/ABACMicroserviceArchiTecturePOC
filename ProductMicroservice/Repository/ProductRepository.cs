using DotNetCoreAPIMicroservice_POC.Models;
using Microsoft.EntityFrameworkCore;
using ProductMicroservice.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreAPIMicroservice_POC.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(int productId)
        {
            Product prod = ProductData.products.Where(x => x.Id == productId).FirstOrDefault();
            ProductData.products.Remove(prod);
        }

        public Product GetProductByID(int productId)
        {
            return ProductData.products.Where(x => x.Id == productId).FirstOrDefault();
        }

        public List<Product> GetProducts()
        {
            return ProductData.products;
        }

        public void InsertProduct(Product product)
        {
            ProductData.products.Add(product);
        }
    }
}
