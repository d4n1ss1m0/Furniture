using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Furniture.Views
{
    /// <summary>
    /// Логика взаимодействия для AddFurnitureToOrder.xaml
    /// </summary>
    public partial class AddFurnitureToOrder : UserControl
    {
        public AddFurnitureToOrder()
        {
            InitializeComponent();
            
            ObservableCollection<Models.Furniture> Furnitures = new ObservableCollection<Models.Furniture>();
            using (FurnitureContext db = new FurnitureContext())
            {
                foreach (Models.Furniture furniture in db.Furnitures)
                {
                    //Console.WriteLine("{0}.{1} - {2}", seller.IDseller, seller.FirstName, seller.LastName);
                    Furnitures.Add(furniture);
                }
            }
            foreach (Models.Furniture furniture in Furnitures)
            {
                var Controll = new Controlls.FurnitureControll(furniture);
                Panel.Children.Add(Controll);
            }
        }
    }
}
