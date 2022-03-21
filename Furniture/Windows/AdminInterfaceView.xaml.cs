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
    /// Логика взаимодействия для AdminInterfaceView.xaml
    /// </summary>
    public partial class AdminInterfaceView : Window
    {
        public AdminInterfaceView(Store.NavigationStore navigationStore)
        {
            InitializeComponent();
            ViewModels.AdminInterfaceViewModel vm = new ViewModels.AdminInterfaceViewModel(navigationStore);
            DataContext = vm;
        }
    }
}
