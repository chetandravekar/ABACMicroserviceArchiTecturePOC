
using DotNetCoreAPIMicroservice_POC.Models;
using System.Collections.Generic;

namespace DotNetCoreAPIMicroservice_POC.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductByID(int product);
        void InsertProduct(Product product);
        void DeleteProduct(int productId);
    }
}
