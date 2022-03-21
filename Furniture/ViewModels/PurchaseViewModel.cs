using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Commands;
using Furniture.Store;
using Furniture.Models;
using System.Windows;

namespace Furniture.ViewModels
{
    public class PurchaseViewModel : ViewModelBase
    {
        private static ObservableCollection<PurchaseItem> purchases;
        public PurchaseViewModel(NavigationStore navigationStore)
        {
            Purchases = new ObservableCollection<PurchaseItem>();
            Save = new SmartCommand(() => {
                using (FurnitureContext db = new FurnitureContext())
                {
                    foreach (var purchase in Purchases)
                    {
                        DateTime tempDate;
                        if (DateTime.TryParse(purchase.Date, out tempDate))
                        {
                            var temp = db.Purchase.Where(p => p.IDPurchase == purchase.Purchase.IDPurchase).First();
                            temp.DateRecord = DateTime.Parse(purchase.Date);
                            db.SaveChanges();

                        }
                        else if (purchase.Date!= "Добавьте дату")
                        {
                            MessageBox.Show("Предупреждение: данные, указанные не в виде даты сохранены не будут!");
                        }
                    }
                }
            });

            using (FurnitureContext db = new FurnitureContext())
            {
                var t = db.Purchase;
                foreach (var purchase in db.Purchase)
                {
                    //Console.WriteLine("{0}.{1} - {2}", seller.IDseller, seller.FirstName, seller.LastName);
                    Purchases.Add(new PurchaseItem(purchase));
                }
            }
            //NavigateSellerCommand = new NavigateCommand<SellerViewModel>(navigationStore, () => new SellerViewModel(navigationStore));
            
            
        }

        public static ICommand Save { get; set; }
        public static ObservableCollection<PurchaseItem> Purchases { get => purchases; set => purchases = value;  }
    }
}
