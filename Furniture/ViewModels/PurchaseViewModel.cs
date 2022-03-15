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

namespace Furniture.ViewModels
{
    public class PurchaseViewModel : ViewModelBase
    {
        private static ObservableCollection<PurchaseItem> purchases;
        public PurchaseViewModel(NavigationStore navigationStore)
        {
            Purchases = new ObservableCollection<PurchaseItem>();
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
            /*
            Add = new SmartCommand(() => {
                new Windows.AddApplicationView().Show();
            });*/
        }
        public static ObservableCollection<PurchaseItem> Purchases { get => purchases; set => purchases = value; }
    }
}
