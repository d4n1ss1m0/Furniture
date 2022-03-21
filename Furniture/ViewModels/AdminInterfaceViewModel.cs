using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Commands;
using Furniture.Store;
using Furniture.Models;
using System.Windows;
using Furniture.ViewModels;
using Furniture.Windows;
using System.ComponentModel;

namespace Furniture.ViewModels
{
    public class AdminInterfaceViewModel
    {
        NavigationStore _navigationStore;
        public ICommand Seller { get; set; }
        public ICommand Delivery { get; set; }
        public ICommand Book { get; set; }
        public ICommand Storage { get; set; }
        public ICommand AddUser { get; set; }
        public ICommand Tables { get; set; }
        public AdminInterfaceViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            Seller = new SmartCommand(() => {
                new SellerInterface(navigationStore).Show();
            });
            Delivery = new SmartCommand(() => {
                new DeliveryInterface(navigationStore).Show();
            });
            Storage = new SmartCommand(() => {
                new StorageInterface(navigationStore).Show();
            });
            Book = new SmartCommand(() => {
                new BookKeeperInterface(navigationStore).Show();
            });
            AddUser = new SmartCommand(() => {
                new AddUserView().Show();
            });
            Tables = new SmartCommand(() => {
                new TableInteface().Show();
            });
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            new Windows.AdminInterfaceView(_navigationStore).Show();        
        }
    }
}
