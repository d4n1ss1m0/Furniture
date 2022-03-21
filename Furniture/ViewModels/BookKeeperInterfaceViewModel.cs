using Furniture.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Furniture.ViewModels
{
    public class BookKeeperInterfaceViewModel
    {
        private readonly NavigationStore _navigationStore;
        public ApplicationsBookViewModel ApplicationsBookViewModel { get; set; }
        public BookKeeperInterfaceViewModel(NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
            ApplicationsBookViewModel = new ApplicationsBookViewModel(navigationStore);
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
