using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture.Models;

namespace Furniture
{
    public class FurnitureContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source =.\\SQLEXPRESS; Initial Catalog = 'furnitureShop'; Trusted_connection=true;TrustServerCertificate=true;");
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=furnitureShop;Integrated Security=True;TrustServerCertificate=true;");
        }

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Models.Furniture> Furnitures { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Furniture_Bill> Furniture_Bills {get;set;}
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<Furniture_purchase> Furniture_Purchases { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Furniture_Bill>()
            .HasKey(p => new { p.IDbill, p.IDfurniture });
        }

    }
}
