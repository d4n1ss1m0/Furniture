using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture.Models;

namespace Furniture
{
    public class DeliveryItem
    {
        public Delivery delivery { get; set; }
        public Receipt receipt { get; set; }
        public Loaders loader { get; set; }
        public Drivers driver { get; set; }
        public Truck truck { get; set; }
        public string order { get; set; }
        public DeliveryItem(Delivery delivery)
        {
            this.delivery = delivery;
            
            using (FurnitureContext db = new FurnitureContext())
            {
                receipt = db.Receipts.Where(p => p.IDreceipt == delivery.IdReciept).First();
                Brigade brigade = db.Brigades.Where(p => p.IdBrigade == delivery.IdBrigade).First();
                
                loader = db.Loaders.Where(p => p.IDloader == brigade.IdLoader).First();
                driver = db.Drivers.Where(p => p.IDdriver == brigade.IdDriver).First();
                truck = db.Trucks.Where(p => p.IdTruck == brigade.IdTruck).First();

                Bill bill = db.Bills.Where(p => p.IDbill == receipt.IDbill).First();
                var fBills = db.Furniture_Bills.Where(p => p.IDbill == bill.IDbill).ToArray();
                foreach (Furniture_Bill furniture_Bill in fBills)
                {
                    var temp = db.Furnitures.Where(p => p.IDfurniture == furniture_Bill.IDfurniture).First();
                    order += db.Furnitures.Where(p=>p.IDfurniture == furniture_Bill.IDfurniture).First().Name.TrimEnd(' ') + "(" + furniture_Bill.Amount.ToString() + " шт.) \n\r";
                }
            }
        }
    }
}
