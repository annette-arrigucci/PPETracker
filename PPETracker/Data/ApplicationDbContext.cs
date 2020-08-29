using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PPETracker.Models;

namespace PPETracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Canister> Canisters { get; set; }
        public DbSet<GasMask> GasMasks { get; set; }
        public DbSet<Gloves> Gloves { get; set; }
        public DbSet<Goggles> Goggles { get; set; }
        public DbSet<HandSanitizer> HandSanitizers { get; set; }
        public DbSet<Mask> Masks { get; set; }
        public DbSet<Wipes> Wipes { get; set; }
        public DbSet<FaceShield> FaceShields { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentProduct> ShipmentProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
        }
    }
}
