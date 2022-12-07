using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Models;

namespace E_Auction.Services.ProductService
{
    public class ProductService : IProductService
    {
        List<Product> _data;
        public ProductService()
        {
            List<Product> data = SeedData();
            _data = data;
        }
        public List<Product> SeedData()
        {
            Product p1 = new Product()
                {
                    Id = 1,
                    Name = "Mona Lisa",
                    ShortDescription = "jdklaf",
                    DetailedDescription = "hfdjkashfsak",
                    Category = Category.Painting,
                    StartingPrice = 100.00,
                    BidEndDate = new DateTime(2011, 6,10),
                };
            Product p2 = new Product()
                {
                    Id = 2,
                    Name = "Christmas",
                    ShortDescription = "Christmas tree orna.",
                    DetailedDescription = "hfdjkashfsak",
                    Category = Category.Ornament,
                    StartingPrice = 90.00,
                    BidEndDate = new DateTime(2011, 7,10),
                };
            Product p3 = new Product()
                {
                    Id = 3,
                    Name = "Halloween",
                    ShortDescription = "halloween tree orna.",
                    DetailedDescription = "hfdjkashfsak",
                    Category = Category.Ornament,
                    StartingPrice = 80.00,
                    BidEndDate = new DateTime(2011, 7,10),
                };
            Product p4 = new Product()
                {
                    Id = 4,
                    Name = "Naked Lady",
                    ShortDescription = "lorem500",
                    DetailedDescription = "hfdjkashfsak",
                    Category = Category.Sculptor,
                    StartingPrice = 50.00,
                    BidEndDate = new DateTime(2011, 7,10),
                };
            Product p5 = new Product()
                {
                    Id = 5,
                    Name = "Naked Man",
                    ShortDescription = "Christmas tree orna.",
                    DetailedDescription = "hfdjkashfsak",
                    Category = Category.Sculptor,
                    StartingPrice = 60.00,
                    BidEndDate = new DateTime(2011, 7,10),
                };


            List<Product> tempDatabase = new List<Product>();
            tempDatabase.Add(p1);
            tempDatabase.Add(p2);
            tempDatabase.Add(p3);
            tempDatabase.Add(p4);
            tempDatabase.Add(p5);

            return tempDatabase;
        }
        public async Task<List<Product>> AddProduct()
        {
            throw new NotImplementedException();
        }


        public async Task<List<Product>> DeleteProduct(int id)
        {
            Product product = _data.FirstOrDefault(p => p.Id == id);
            if(product != null)
            {
                _data.Remove(product);
            }

            return _data;
        }

        public async Task<Product> GetProductById(int id)
        {
            Product product = _data.FirstOrDefault(p => p.Id == id);

            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            return _data;
        }
    }
}