using Furniture.Commands;
using Furniture.Models;
using Furniture.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Furniture.ViewModels
{
    public class AddPurchaseViewModel
    {
        public ICommand Close { get; set; }
        public ICommand Go { get; set; }
        public Application Application { get; set; }
        public string furniture { get; set; }
        public int amount { get; set; }
        public AddPurchaseViewModel(Application application)
        {

            Application = application;
            using (FurnitureContext db = new FurnitureContext())
            {
                furniture = db.Furnitures.Where(p => p.IDfurniture == application.IDfurniture).First().Name.TrimEnd(' ');
                amount = application.Amount;
            }
            Go = new SmartCommand(() => {
                using (FurnitureContext db = new FurnitureContext())
                {
                    Purchase purchase = new Purchase();
                    purchase.IDfurniture = Application.IDfurniture;
                    purchase.Amount = Application.Amount;
                    purchase.DatePurchase = DateTime.Now;
                    purchase.Sum = db.Furnitures.Where(p => p.IDfurniture == purchase.IDfurniture).First().Price * purchase.Amount;
                    purchase.IDPurchase = db.Purchase.AsEnumerable().Last().IDPurchase + 1;
                    db.Purchase.Add(purchase);
                    db.Applications.Where(p => p.IDapplication == application.IDapplication).First().Status = "Одобрено";
                    db.SaveChanges();
                    ApplicationsBookViewModel.Update.Execute("update");
                    Close.Execute("close");
                }
            });
        }
    }
}
