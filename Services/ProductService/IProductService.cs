using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Models;

namespace E_Auction.Services.ProductService
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<List<Product>> AddProduct();
        Task<List<Product>> DeleteProduct(int id);
    }
}