using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Furniture.Windows
{
    /// <summary>
    /// Логика взаимодействия для SellerInterface.xaml
    /// </summary>
    public partial class SellerInterface : Window
    {
        public SellerInterface(Store.NavigationStore navigationStore)
        {
            InitializeComponent();
            ViewModels.SellerInterfaceViewModel vm = new ViewModels.SellerInterfaceViewModel(navigationStore);
            DataContext = vm;
            Closing += vm.OnWindowClosing;
        }

        
    }
}
