using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Drawing.Imaging;


namespace Furniture.Controlls
{
    /// <summary>
    /// Логика взаимодействия для FurnitureControll.xaml
    /// </summary>
    /// 

    public static class BitmapConversion
    {
        public static BitmapSource BitmapToBitmapSource(Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                          source.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
        }
    }

    public partial class FurnitureControll : UserControl
    {
        private RelayCommand parameterizedCommand;
        private string amountText="0";
        private Models.Furniture furniture;
        public BitmapSource Path { get; set; }
        public FurnitureControll()
        {
            InitializeComponent();
        }

        public FurnitureControll(Models.Furniture f)
        {
            
            furniture = f;
            DataContext = this;
            Bitmap bitmap = (Bitmap)Bitmap.FromFile(@"..\..\Image\Closet.png", true);
            switch (f.Category.Replace(" ",string.Empty))
            {
                case "Шкаф": bitmap = (Bitmap)Bitmap.FromFile(@"..\..\Image\Closet.png", true);  break;
                case "Стул": bitmap = (Bitmap)Bitmap.FromFile(@"..\..\Image\Chair.png", true);  break;
                case "Диван": bitmap = (Bitmap)Bitmap.FromFile(@"..\..\Image\Couch.png", true);  break;
                case "Стол": bitmap = (Bitmap)Bitmap.FromFile(@"..\..\Image\OfficeTable.png", true); break;
                case "Кровать": bitmap = (Bitmap)Bitmap.FromFile(@"..\..\Image\Bed.png", true); break;
            }
            Path = BitmapConversion.BitmapToBitmapSource(bitmap);
            parameterizedCommand = new RelayCommand(DoParameterisedCommand);
            parameterizedCommand.IsEnabled = true;
            InitializeComponent();
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
