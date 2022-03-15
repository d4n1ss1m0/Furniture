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
    public class SellerViewModel:ViewModelBase
    {
        private ObservableCollection<Models.Seller> sellers;

        private Models.Seller curentSeller;

        public SellerViewModel(NavigationStore navigationStore)
        {
            NavigateFurnitureCommand = new NavigateCommand<FurnitureViewModel>(navigationStore, ()=> new FurnitureViewModel(navigationStore));
            Sellers = new ObservableCollection<Models.Seller>();
            using (FurnitureContext db = new FurnitureContext())
            {
                foreach(Models.Seller seller in db.Sellers)
                {
                    //Console.WriteLine("{0}.{1} - {2}", seller.IDseller, seller.FirstName, seller.LastName);
                    Sellers.Add(seller);
                }
            }
        }

        public ObservableCollection<Seller> Sellers { get => sellers; set => sellers = value; }
        public Seller CurentSeller {
            get
            {
                return curentSeller;
            }
            set
            {
                curentSeller = value;
                OnPropertyChanged("CurentSeller");
            }
        }

        public ICommand NavigateFurnitureCommand { get; }
    }
}
