using PPETracker.Models;
using System.Linq;

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
    }
}
