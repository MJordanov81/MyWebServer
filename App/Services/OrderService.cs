namespace App.Services
{
    using Context;
    using Context.Models;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Views.ViewModels.Orders;
    using Constants;

    public class OrderService : IOrderService
    {
        public int Add(string username, Cart cart)
        {
            IDictionary<int, int> products = cart.OrderedItems;

            using (AppDbContext db = new AppDbContext())
            {
                int userId = db.Users
                    .Where(u => u.Username == username)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                Order order = new Order()
                {
                    CreationDate = DateTime.UtcNow,
                    UserId = userId
                };
                db.Add(order);

                db.SaveChanges();

                foreach (KeyValuePair<int, int> product in products)
                {
                    db.Add(new ProductOrder()
                    {
                        ProductId = product.Key,
                        OrderId = order.Id,
                        Quantity = product.Value
                    });
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return order.Id;
            }
        }

        public OrderDetailsViewModel Details(int orderId)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Order order = db.Orders
                    .Include(o => o.Products)
                    .ThenInclude(p => p.Product)
                    .FirstOrDefault(o => o.Id == orderId);

                try
                {
                    return new OrderDetailsViewModel()
                    {
                        Id = order.Id,
                        Products = order.Products
                        .Select(po => Tuple.Create(po.Product.Name, po.Quantity, po.Product.Price))
                        .ToList()
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return null;
            }
        }

        public IList<ShortOrderDetailsViewModel> ShortDetailsList(string username)
        {
            using (AppDbContext db = new AppDbContext())
            {
                int userId = db.Users.Where(u => u.Username == username).Select(u => u.Id).FirstOrDefault();

                try
                {
                    return db.Orders.Include(o => o.Products).ThenInclude(p => p.Product)
                        .Where(o => o.UserId == userId).Select(o => new ShortOrderDetailsViewModel()
                        {
                            Id = o.Id,
                            CreationDate = o.CreationDate.ToString("dd-MM-yyyy"),
                            TotalAmount = $"{o.Products.Sum(p => p.Quantity * p.Product.Price)}"
                        }).ToList();
                }
                catch (Exception)
                {
                    throw new InvalidOperationException(ErrorConstants.UnableToCreateModel);
                }
            }
        }
    }
}
