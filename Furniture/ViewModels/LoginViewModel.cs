using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Commands;
using Furniture.Store;

namespace Furniture.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        public LoginViewModel(NavigationStore navigationStore)
        {
            ShowSellerInterface = new SmartCommand(() => 
            { 
                NavigationStore navigation_Store = new NavigationStore();
                new Windows.SellerInterface(navigation_Store).Show(); 
                App.Current.MainWindow.Close(); 
            });
            /*
            NavigateFurnitureCommand = new NavigateCommand<FurnitureViewModel>(navigationStore, () => new FurnitureViewModel(navigationStore));
            NavigateSellerCommand = new NavigateCommand<SellerViewModel>(navigationStore, () => new SellerViewModel(navigationStore));
            NavigateAddFurnitureToOrderCommand = new NavigateCommand<AddFurnitureToOrderViewModel>(navigationStore, () => new AddFurnitureToOrderViewModel(navigationStore));*/
        }

        public ICommand ShowSellerInterface { get; }

        public ICommand NavigateSellerCommand { get; }
        public ICommand NavigateFurnitureCommand { get; }
        public ICommand NavigateAddFurnitureToOrderCommand { get; }

    }
}
