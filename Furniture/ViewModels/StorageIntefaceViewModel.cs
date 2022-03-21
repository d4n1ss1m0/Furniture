using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Furniture.Store;

namespace Furniture.ViewModels
{
    class StorageInterfaceViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private ViewModelBase furnitureViewModel;
        private ViewModelBase purchaseViewModel;
        private ViewModelBase applicationsViewModel;
        //private ViewModelBase addFurnitureToOrderViewModel;

        public StorageInterfaceViewModel(NavigationStore navigationStore)
        {
            
            _navigationStore = navigationStore;
            /*
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _navigationStore.CurrentViewModel = new AddFurnitureToOrderViewModel(navigationStore);
            */
            FurnitureViewModel = new FurnitureViewModel(navigationStore);
            ApplicationsViewModel = new ApplicationsViewModel(navigationStore);
            PurchaseViewModel = new PurchaseViewModel(navigationStore);
            //AddFurnitureToOrderViewModel = new AddFurnitureToOrderViewModel(navigationStore);
        }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ViewModelBase FurnitureViewModel { get => furnitureViewModel; set => furnitureViewModel = value; }
        public ViewModelBase ApplicationsViewModel { get => applicationsViewModel; set => applicationsViewModel = value; }
        public ViewModelBase PurchaseViewModel { get => purchaseViewModel; set => purchaseViewModel = value; }

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
