using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Furniture.Store;
using Furniture.ViewModels;

namespace Furniture
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Models.Accounts acc;
        protected override void OnStartup(StartupEventArgs e)
        {
            
            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);
            
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
