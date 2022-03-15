using Furniture.Commands;
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
    class AddFurnitureToOrderViewModel:ViewModelBase
    {
        private static List<Cart> cart;
        private ObservableCollection<Models.Furniture> furnitures;
        //private RelayCommand parameterizedCommand;

        private Models.Furniture curentFurniture;
        public ICommand NavigateCreateOrderCommand { get; }

        public AddFurnitureToOrderViewModel(NavigationStore navigationStore)
        {
            cart = new List<Cart>();
            NavigateCreateOrderCommand = new NavigateCommand<MKOrderViewModel>(navigationStore, () => new MKOrderViewModel(navigationStore,cart));
            Furnitures = new ObservableCollection<Models.Furniture>();
            var kekl = new Views.AddFurnitureToOrder();           
            
            using (FurnitureContext db = new FurnitureContext())
            {
                foreach (Models.Furniture furniture in db.Furnitures)
                {
                    //Console.WriteLine("{0}.{1} - {2}", seller.IDseller, seller.FirstName, seller.LastName);
                    Furnitures.Add(furniture);
                }
            }
        }

        public static List<Cart> Cart { get => cart; set => cart = value; }
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

        
    }
}
