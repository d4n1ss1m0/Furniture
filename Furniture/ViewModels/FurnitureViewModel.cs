using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Commands;
using Furniture.Store;
using Furniture.Models;

namespace Furniture.ViewModels
{
    class FurnitureViewModel : ViewModelBase
    {
        private ObservableCollection<FurnitureItem> furnitures;

        private Models.Furniture curentFurniture;

        public FurnitureViewModel(NavigationStore navigationStore)
        {
            NavigateSellerCommand = new NavigateCommand<SellerViewModel>(navigationStore, ()=>new SellerViewModel(navigationStore));
            Furnitures = new ObservableCollection<FurnitureItem>();
            using (FurnitureContext db = new FurnitureContext())
            {
                DateTime date1 = DateTime.Parse("01." + DateTime.Now.ToString("MM.yyyy"));
                DateTime date2 = date1.AddMonths(1);
                Dictionary<int, Models.Furniture> furn = new Dictionary<int, Models.Furniture>();
                foreach(Models.Furniture f in db.Furnitures)
                {
                    furn.Add(f.IDfurniture, f);
                }


                            
                var furniture_Bills = (from f in db.Furnitures
                                       join f_b in db.Furniture_Bills on f.IDfurniture equals f_b.IDfurniture
                                       join r in db.Receipts on f_b.IDbill equals r.IDreceipt
                                       where r.RecieveDate >= date1 && r.RecieveDate <= date2
                                       group f_b by f_b.IDfurniture into g
                                       select new { IDfuniture = g.Key, Sum = g.Sum(f => f.Amount) }).ToArray();
                foreach(var f in furniture_Bills)
                {
                    //   FurnitureItem f = new FurnitureItem();
                    FurnitureItem i = new FurnitureItem();
                    i.IsEnough = (f.Sum/2) < furn[f.IDfuniture].Amount;
                    i.Furniture = furn[f.IDfuniture];
                    Furnitures.Add(i);
                }
            }
            
        }

        public ObservableCollection<FurnitureItem> Furnitures { get => furnitures; set => furnitures = value; }
        public Models.Furniture CurentFurniture
        {
            get
            {
                return curentFurniture;
            }
            set
            {
                curentFurniture = value;
                OnPropertyChanged("CurentSeller");
            }
        }

        public ICommand NavigateSellerCommand { get; }

        
    }
}
