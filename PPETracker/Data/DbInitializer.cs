using Microsoft.AspNetCore.Identity;
using PPETracker.Models;
using System.Linq;
using System.Security.Claims;

namespace PPETracker.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }
            var categories = new Category[]
            {
                new Category
                {
                    Name = "Canister",
                    IsActive = true
                },
                new Category
                {
                    Name = "Gas Mask",
                    IsActive = true
                },
                new Category
                {
                    Name = "Gloves",
                    IsActive = true
                },
                new Category
                {
                    Name = "Hand Sanitizer",
                    IsActive = true
                },
                new Category
                {
                    Name = "Mask",
                    IsActive = true
                },
                new Category
                {
                    Name = "Wipes",
                    IsActive = true
                }
                ,
                new Category
                {
                    Name = "Goggles",
                    IsActive = true
                },
                new Category
                {
                    Name = "FaceShield",
                    IsActive = true
                }
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();
        }

        public static void CreateUsers(UserManager<ApplicationUser> userManager)
        {
            //create two admin users
            if (userManager.FindByEmailAsync("john.tom.abc@gmail.com").Result == null)
            {
                var user1 = new ApplicationUser();
                user1.Email = "john.tom.abc@gmail.com";
                user1.UserName = "john.tom.abc@gmail.com";
                var password = "Abc123$";
                user1.IsAdmin = true;
                IdentityResult result = userManager.CreateAsync(user1, password).Result;
            }

            if (userManager.FindByEmailAsync("annette.arrigucci@gmail.com").Result == null)
            {
                var user2 = new ApplicationUser();
                user2.Email = "annette.arrigucci@gmail.com";
                user2.UserName = "annette.arrigucci@gmail.com";
                var password = "Abc123$";
                user2.IsAdmin = true;
                IdentityResult result = userManager.CreateAsync(user2, password).Result;
            }
        }
    }
}
