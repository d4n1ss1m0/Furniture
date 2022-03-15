 using Furniture.Commands;
using Furniture.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Models;
using System.Collections.ObjectModel;

namespace Furniture.ViewModels
{
    class MKOrderViewModel : ViewModelBase
    {
        public DeliverySheduleItem SelectedDelivery { get; set; }
        private DateTime selectedDate;
        private static List<Cart> cart;
         
        private ObservableCollection<DeliverySheduleItem> delivery;
        //private RelayCommand parameterizedCommand;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }

        public DateTime SelectedDate 
        {
            get
            {
                return selectedDate;
            }
            set
            {
                Delivery.Clear();
                selectedDate = value;
                using (FurnitureContext db = new FurnitureContext())
                {
                    DateTime timer = new DateTime();
                    timer = timer.AddHours(11);//Допустим, доставка работает с 11 до 18
                    //var dbDelivery = db.Delivery.Where(p=> p.Date.ToShortDateString() == curentDate.ToShortDateString() );
                    while (timer <= new DateTime().AddHours(18))
                    {
                        var dbDelivery = db.Delivery.Where(p => (p.Date == selectedDate && p.Time == timer.TimeOfDay));
                        bool isAvalible = true;
                        if (dbDelivery.Count()!=0)
                        {
                            isAvalible = false;
                        }
                        Delivery.Add(new DeliverySheduleItem(selectedDate.ToShortDateString(), timer.ToShortTimeString(), isAvalible));
                        timer= timer.AddMinutes(30);
                    }
                }
            } 
        }

        private Models.Furniture curentFurniture;
        public ICommand NavigateBackCommand { get; }

        public MKOrderViewModel(NavigationStore navigationStore, List<Cart> Cart)
        {
            cart = Cart;

            //NavigateBackCommand = new NavigateCommand<AddFurnitureToOrderViewModel>(navigationStore, () => new AddFurnitureToOrderViewModel(navigationStore));
            NavigateBackCommand = new SmartCommand(() =>
            {
                AddOrder();
                new NavigateCommand<AddFurnitureToOrderViewModel>(navigationStore, () => new AddFurnitureToOrderViewModel(navigationStore)).Execute("execute");
            });
            Delivery = new ObservableCollection<DeliverySheduleItem>();
            SelectedDate = DateTime.Now;
            var kekl = new Views.AddFurnitureToOrder();           
        }

        public void AddOrder()
        {
            using (FurnitureContext db = new FurnitureContext())
            {
                //Создаем чек
                Bill bill = new Bill();
                bill.IDbill = db.Bills.AsEnumerable().Last().IDbill + 1;
                bill.IDSeller = 1;
                //Создаем отношение товара к чеку
                List<Furniture_Bill> rangeFurnitureBills = new List<Furniture_Bill>();
                //считаем итог стоимость, заполняем отношение
                decimal sum = 0;
                foreach(Cart e in cart)
                {
                    sum += e.Cost;
                    Furniture_Bill furnitureBill = new Furniture_Bill();
                    furnitureBill.Amount = e.AmountCart;
                    furnitureBill.IDfurniture = e.Furniture.IDfurniture;
                    furnitureBill.IDbill = bill.IDbill;
                    //вычет
                    db.Furnitures.Find(e.Furniture.IDfurniture).Amount -= e.AmountCart;
                    rangeFurnitureBills.Add(furnitureBill);        
                    
                }
                bill.Sum = sum;
                db.Bills.Add(bill);
                db.Furniture_Bills.AddRange(rangeFurnitureBills);
                //Создаем квитанцию
                Receipt receipt = new Receipt();
                receipt.IDreceipt = db.Receipts.AsEnumerable().Last().IDreceipt + 1;
                receipt.ReceiveTime = TimeSpan.Parse(DateTime.Now.ToShortTimeString());
                receipt.RecieveDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                receipt.Address = "kekw kekw kekl";
                receipt.IDbill = bill.IDbill;
                receipt.Phone = Phone;
                receipt.FirstName = FirstName;
                receipt.LastName = LastName;
                receipt.MiddleName = MiddleName;
                receipt.IDarea = 1;
                db.Receipts.Add(receipt);
                Delivery newDelivery = new Delivery();
                newDelivery.IdReciept = receipt.IDreceipt;
                newDelivery.Date = DateTime.Parse(SelectedDelivery.Date);
                newDelivery.Time = TimeSpan.Parse(SelectedDelivery.Time);
                db.Delivery.Add(newDelivery);
                db.SaveChanges();
            }
        }


        public static List<Cart> Cart { get => cart; set => cart = value; }
        //public ObservableCollection<Models.Furniture> Furnitures { get => furnitures; set => furnitures = value; }
        public Models.Furniture CurentFurniture
        {
            get
            {
                return curentFurniture;
            }
            set
            {
                curentFurniture = value;
                OnPropertyChanged("CurentSeller");
            }
        }

        public ObservableCollection<DeliverySheduleItem> Delivery { get => delivery; set => delivery = value; }
    }
}
