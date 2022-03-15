using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Commands;
using Furniture.Store;

namespace Furniture.ViewModels
{
    class FurnitureViewModel : ViewModelBase
    {
        private ObservableCollection<Models.Furniture> furnitures;

        private Models.Furniture curentFurniture;

        public FurnitureViewModel(NavigationStore navigationStore)
        {
            NavigateSellerCommand = new NavigateCommand<SellerViewModel>(navigationStore, ()=>new SellerViewModel(navigationStore));
            Furnitures = new ObservableCollection<Models.Furniture>();
            using (FurnitureContext db = new FurnitureContext())
            {
                foreach (Models.Furniture furniture in db.Furnitures)
                {
                    //Console.WriteLine("{0}.{1} - {2}", seller.IDseller, seller.FirstName, seller.LastName);
                    Furnitures.Add(furniture);
                }
            }
            
        }

        public ObservableCollection<Models.Furniture> Furnitures { get => furnitures; set => furnitures = value; }
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
