namespace App.Services
{
    using Constants;
    using Context;
    using Context.Models;
    using Contracts;
    using System;
    using System.Linq;
    using Views.ViewModels.User;

    public class UserService : IUserService
    {
        public bool Add(string username, string password)
        {
            using (AppDbContext db = new AppDbContext())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    return false;
                }

                db.Add(new User()
                {
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.UtcNow
                });
                db.SaveChanges();

                return true;
            }
        }

        public bool UserExists(string username, string password)
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.Users.Any(u => u.Username == username && u.Password == password);
            }
        }

        public UserProfileViewModel GetProfile(string username)
        {
            using (AppDbContext db = new AppDbContext())
            {
                if (!db.Users.Any(u => u.Username == username))
                {
                    throw new InvalidOperationException(ErrorConstants.MisingProductInDb);
                }

                return db.Users
                    .Where(u => u.Username == username)
                    .Select(u => new UserProfileViewModel()
                    {
                        Username = u.Username,
                        RegistrationDate = u.RegistrationDate.ToString("dd-MM-yyyy"),
                        OrdersCount = u.Orders.Count.ToString()
                    })
                    .FirstOrDefault();
            }
        }
    }
}
