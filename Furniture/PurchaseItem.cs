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
        private string date;
        public PurchaseItem(Purchase purchase)
        {
            Purchase = purchase;
            using (FurnitureContext db = new FurnitureContext())
            {
                Models.Furniture f = db.Furnitures.Where(p => p.IDfurniture == purchase.IDfurniture).First();
                Supplier = db.Suppliers.Where(p => p.IDsupplier == f.IDsupplier).First();
                if (purchase.DateRecord.HasValue)
                {
                    Date = purchase.DateRecord.Value.ToShortDateString();
                }
                else
                {
                    Date = "Добавьте дату";
                }

                
            }
        }
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                ViewModels.PurchaseViewModel.Save.Execute("save");
            }
        }
        public Models.Purchase Purchase { get; set; }
        public Models.Supplier Supplier{get;set;}
    }
}
