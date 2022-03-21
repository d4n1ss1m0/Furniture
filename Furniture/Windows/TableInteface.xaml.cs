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
    /// Логика взаимодействия для TableInteface.xaml
    /// </summary>
    public partial class TableInteface : Window
    {
        private string selectedItem;
        public TableInteface()
        {
            InitializeComponent();
            combo.SelectedIndex = 0;

        }


        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (FurnitureContext db = new FurnitureContext())
            {
                ComboBox comboBox = (ComboBox)sender;
                ComboBoxItem select = (ComboBoxItem)comboBox.SelectedItem;
                switch (select.Content.ToString())
                {
                    case "Аккаунты":
                        testGrid.ItemsSource = db.Accounts.ToList();
                        break;
                    case "Заявки":
                        testGrid.ItemsSource = db.Applications.ToList();
                        break;
                    case "Районы":
                        testGrid.ItemsSource = db.Area.ToList();
                        break;
                    case "Чеки":
                        testGrid.ItemsSource = db.Bills.ToList();
                        break;
                    case "Бригады":
                        testGrid.ItemsSource = db.Brigades.ToList();
                        break;
                    case "Категории":
                        testGrid.ItemsSource = db.Categories.ToList();
                        break;
                    case "Доставки":
                        testGrid.ItemsSource = db.Deliveries.ToList();
                        break;
                    case "Водители":
                        testGrid.ItemsSource = db.Drivers.ToList();
                        break;
                    case "Товары":
                        testGrid.ItemsSource = db.Furnitures.ToList();
                        break;
                    case "Товары_Чеки":
                        testGrid.ItemsSource = db.Furniture_Bills.ToList();
                        break;
                    case "Товары_Закупки":
                        testGrid.ItemsSource = db.Furniture_Purchases.ToList();
                        break;
                    case "Грузчики":
                        testGrid.ItemsSource = db.Loaders.ToList();
                        break;
                    case "Закупки":
                        testGrid.ItemsSource = db.Purchase.ToList();
                        break;
                    case "Квитанции":
                        testGrid.ItemsSource = db.Receipts.ToList();
                        break;
                    case "Продавцы":
                        testGrid.ItemsSource = db.Sellers.ToList();
                        break;
                    case "Поставщики":
                        testGrid.ItemsSource = db.Suppliers.ToList();
                        break;
                    case "Грузовики":
                        testGrid.ItemsSource = db.Trucks.ToList();
                        break;

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (FurnitureContext db = new FurnitureContext())
            {
                db.SaveChanges();
            }
        }
    }
}
