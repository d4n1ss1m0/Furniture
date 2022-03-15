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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Furniture.Controlls
{
    /// <summary>
    /// Логика взаимодействия для FurnitureControll.xaml
    /// </summary>
    public partial class FurnitureControll : UserControl
    {
        private RelayCommand parameterizedCommand;
        private string amountText="0";
        private Models.Furniture furniture;
        public FurnitureControll()
        {
            InitializeComponent();
        }

        public FurnitureControll(Models.Furniture f)
        {
            InitializeComponent();
            furniture = f;
            DataContext = this;
            parameterizedCommand = new RelayCommand(DoParameterisedCommand);
            parameterizedCommand.IsEnabled = true;
        }

        public string AmountText { get => amountText; set => amountText = value; }
        public Models.Furniture Furniture { get => furniture; set => furniture = value; }

        private void DoParameterisedCommand()
        {
            Console.WriteLine("CERFFFFFF");
            //Cart.Add(new Cart(parameter,))                      
            ViewModels.AddFurnitureToOrderViewModel.Cart.Add(new Cart(furniture,Convert.ToInt32(amountText)));
        }

        public RelayCommand ParameterisedCommand
        {
            get { return parameterizedCommand; }
        }

    }
}
