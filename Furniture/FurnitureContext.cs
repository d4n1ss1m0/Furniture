using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture.Models;

namespace Furniture
{
    public class FurnitureContext : DbContext
    {
        public FurnitureContext(string login, string password) : base("Data Source =.\\SQLEXPRESS; Initial Catalog = 'Furniture'; User Id = " + login + "; Password=" + password)
        {

        }

        public FurnitureContext() : base("Data Source =.\\SQLEXPRESS; Initial Catalog = 'furnitureShop'; Trusted_connection=true;")
        {

        }

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Models.Furniture> Furnitures { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Furniture_Bill> Furniture_Bills {get;set;}
        public DbSet<Delivery> Delivery { get; set; }


    }
}
