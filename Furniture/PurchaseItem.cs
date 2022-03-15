using Furniture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture
{
    public class PurchaseItem
    {
        public PurchaseItem(Purchase purchase)
        {
            Purchase = purchase;
            using (FurnitureContext db = new FurnitureContext())
            {
                Models.Furniture f = db.Furnitures.Where(p => p.IDfurniture == purchase.IDPurchase).First();
                Supplier = db.Suppliers.Where(p => p.IDsupplier == f.IDsupplier).First();
            }
        }

        public Models.Purchase Purchase { get; set; }
        public Models.Supplier Supplier{get;set;}
    }
}
