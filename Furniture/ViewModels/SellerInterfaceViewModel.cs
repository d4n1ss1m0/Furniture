using Furniture.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Furniture.ViewModels
{
    public class SellerInterfaceViewModel: ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private ViewModelBase sellerViewModel;
        private ViewModelBase ordersViewModel;
        //private ViewModelBase addFurnitureToOrderViewModel;

        public SellerInterfaceViewModel(NavigationStore navigationStore)
        {
            
            _navigationStore = navigationStore;           
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _navigationStore.CurrentViewModel = new AddFurnitureToOrderViewModel(navigationStore);
            
            SellerViewModel = new SellerViewModel(navigationStore);
            OrdersViewModel = new OrdersViewModel(navigationStore);
            //AddFurnitureToOrderViewModel = new AddFurnitureToOrderViewModel(navigationStore);
        }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ViewModelBase SellerViewModel { get => sellerViewModel; set => sellerViewModel = value; }
        public ViewModelBase OrdersViewModel { get => ordersViewModel; set => ordersViewModel = value; }

        //public ViewModelBase AddFurnitureToOrderViewModel { get => addFurnitureToOrderViewModel; set => addFurnitureToOrderViewModel = value; }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {

            if (App.acc.role != "Admin")
            {
                App.Current.MainWindow = new MainWindow(_navigationStore);
                App.Current.MainWindow.Show();
            }
            
        }
    }
}
