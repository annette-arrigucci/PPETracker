using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PPETracker.Models;

namespace PPETracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentProduct> ShipmentProducts { get; set; }
    }
}
