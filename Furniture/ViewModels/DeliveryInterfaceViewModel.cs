using Furniture.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Furniture.ViewModels
{
    public class DeliveryInterfaceViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private ViewModelBase deliveryViewModel;
        private ViewModelBase areaViewModel;
        private ViewModelBase applicationsViewModel;

        public DeliveryInterfaceViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            DeliveryViewModel = new DeliveryViewModel(navigationStore);
            AreaViewModel = new AreaViewModel(navigationStore);
            //ApplicationsViewModel = new ApplicationsViewModel(navigationStore);
            //PurchaseViewModel = new PurchaseViewModel(navigationStore);
        }

        public ViewModelBase DeliveryViewModel { get => deliveryViewModel; set => deliveryViewModel = value; }
        public ViewModelBase AreaViewModel { get => applicationsViewModel; set => applicationsViewModel = value; }
        public ViewModelBase PurchaseViewModel { get => areaViewModel; set => areaViewModel = value; }

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
