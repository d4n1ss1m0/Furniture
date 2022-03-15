using Furniture.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture.ViewModels
{
    public class SellerInterfaceViewModel: ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private ViewModelBase sellerViewModel;
        //private ViewModelBase addFurnitureToOrderViewModel;

        public SellerInterfaceViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _navigationStore.CurrentViewModel = new AddFurnitureToOrderViewModel(navigationStore);

            SellerViewModel = new SellerViewModel(navigationStore);
            //AddFurnitureToOrderViewModel = new AddFurnitureToOrderViewModel(navigationStore);
        }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ViewModelBase SellerViewModel { get => sellerViewModel; set => sellerViewModel = value; }
        //public ViewModelBase AddFurnitureToOrderViewModel { get => addFurnitureToOrderViewModel; set => addFurnitureToOrderViewModel = value; }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
