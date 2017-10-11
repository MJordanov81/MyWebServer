namespace App.Services
{
    using Constants;
    using Context;
    using Context.Models;
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Views.ViewModels.Product;

    class ProductService : IProductService
    {
        public bool Add(string name, decimal price, string imageUrl, out int productId)
        {
            using (AppDbContext db = new AppDbContext())
            {
                if (db.Products.Any(p => p.Name == name))
                {
                    productId = 0;
                    return false;
                }

                Product product = new Product()
                {
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl
                };


                db.Add(product);
                db.SaveChanges();

                productId = product.Id;

                return true;
            }
        }

        public bool ExistsProduct(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.Products.Any(p => p.Id == id);
            }
        }

        public ProductDetailsViewModel GetDetails(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                if (!db.Products.Any(p => p.Id == id))
                {
                    throw new InvalidOperationException(ErrorConstants.MisingProductInDb);
                }

                return db.Products.Where(p => p.Id == id).Select(p => new ProductDetailsViewModel()
                {
                    Name = p.Name,
                    Price = $"{p.Price:f2}",
                    ImageUrl = p.ImageUrl
                }).FirstOrDefault();
            }
        }

        public IList<SearchProductsViewModel> GetProducts(string[] query)
        {
            using (AppDbContext db = new AppDbContext())
            {
                IList<SearchProductsViewModel> result = db.Products.Select(
                    p => new SearchProductsViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = $"{p.Price:f2}"
                    }).ToList();

                if (query != null)
                {
                    result = result.Where(p => this.PositiveSearch(query, p.Name)).ToList();
                }

                return result;
            }
        }

        public ProductInCartViewModel GetOrderedProductDetails(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                if (!db.Products.Any(p => p.Id == id))
                {
                    throw new InvalidOperationException(ErrorConstants.MisingProductInDb);
                }

                return db.Products.Where(p => p.Id == id).Select(p => new ProductInCartViewModel()
                {
                    Name = p.Name,
                    Price = p.Price
                }).FirstOrDefault();
            }
        }

        private bool PositiveSearch(string[] query, string productName)
        {
            if (query.Length == 1 && query[0] == "*")
            {
                return true;
            }

            bool result = true;

            foreach (string queryPart in query)
            {
                if (!productName.ToLower().Contains(queryPart.ToLower()))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
