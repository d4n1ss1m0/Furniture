using System;
using Furniture.Store;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Furniture.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            //NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        /*
                private  Page Seller;
                private Page Furniture;
                private Page AddFurnitureToOrder;
                private RelayCommand parameterisedCommand;
                //private Page _curentPage;
                public Page CurentPage
                {
                    get;
                    set;
                }
                public MainViewModel()
                {
                    Seller = new Views.SellersView();
                    Furniture = new Views.FurnitureView();
                    AddFurnitureToOrder = new Views.AddFurnitureToOrderView();
                    parameterisedCommand = new RelayCommand(DoParameterisedCommand);
                    parameterisedCommand.IsEnabled = true;
                    CurentPage = Seller;
                }



                private void DoParameterisedCommand()
                {
                    Console.WriteLine("CERFFFFFF");

                    CurentPage = Furniture;
                    //Cart.Add(new Cart(parameter,))                      
                    //ViewModels.AddFurnitureToOrderViewModel.Cart.Add(new Cart(furniture,Convert.ToInt32(amountText)));
                }

                public RelayCommand ParameterisedCommand
                {
                    get { return parameterisedCommand; }
                }*/
    }
}
