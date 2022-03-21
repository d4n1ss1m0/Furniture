using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Commands;
using Furniture.Store;
using Furniture.Models;
using System.Windows;
using System.Globalization;

namespace Furniture.ViewModels
{
    class FurnitureViewModel : ViewModelBase
    {
        private ObservableCollection<FurnitureItem> furnitures;

        private Models.Furniture curentFurniture;

        public FurnitureViewModel(NavigationStore navigationStore)
        {
            NavigateSellerCommand = new NavigateCommand<SellerViewModel>(navigationStore, ()=>new SellerViewModel(navigationStore));
            Furnitures = new ObservableCollection<FurnitureItem>();
            using (FurnitureContext db = new FurnitureContext())
            {
                DateTime date1 = DateTime.Parse("01." + DateTime.Now.ToString("MM.yyyy"));
                DateTime date2 = date1.AddMonths(-1);
                Dictionary<int, Models.Furniture> furn = new Dictionary<int, Models.Furniture>();
                foreach(Models.Furniture f in db.Furnitures)
                {
                    furn.Add(f.IDfurniture, f);
                }


                     /*       
                var furniture_Bills = (from f in db.Furnitures
                                       join f_b in db.Furniture_Bills on f.IDfurniture equals f_b.IDfurniture
                                       join r in db.Receipts on f_b.IDbill equals r.IDreceipt
                                       where r.RecieveDate >= date2 && r.RecieveDate <= date1
                                       group f_b by f_b.IDfurniture into g
                                       select new { IDfuniture = g.Key, Sum = g.Sum(f => f.Amount) }).ToArray();*/
                foreach(var furniture in furn)
                {
                    //   FurnitureItem f = new FurnitureItem();
                    FurnitureItem i = new FurnitureItem();
                    int temp = 0;
                    try
                    {
                        var furniture_Bills = (from f in db.Furnitures
                                               join f_b in db.Furniture_Bills on f.IDfurniture equals f_b.IDfurniture
                                               join r in db.Receipts on f_b.IDbill equals r.IDreceipt
                                               where r.RecieveDate >= date2 && r.RecieveDate <= date1 && f.IDfurniture == furniture.Key
                                               group f_b by f_b.IDfurniture into g
                                               select new { IDfuniture = g.Key, Sum = g.Sum(f => f.Amount) }).First();
                        temp = furniture_Bills.Sum / 2;
                    }
                    catch
                    {
                        temp = 2;
                    }
                    i.IsEnough = temp < furniture.Value.Amount;
                    i.Furniture = furniture.Value;
                    Furnitures.Add(i);
                }
            }
            CreateReport = new SmartCommand(() =>
            {
                using (FurnitureContext db = new FurnitureContext())
                {
                    //Первый день в году
                    DateTime startDate = DateTime.Parse("01.01." + DateTime.Now.ToString("yyyy"));
                    //Получаем номер предыдущей недели
                    int week = (new GregorianCalendar()).GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday)-1;
                    //Даты недели
                    DateTime date1 = FirstDateOfWeek(Convert.ToInt32(DateTime.Now.ToString("yyyy")), week, CultureInfo.CurrentCulture);
                    DateTime date2 = date1.AddDays(6);
                    //Выборка квитанций, которые были оформлены на прошлой неделе
                    var purchase = db.Purchase.Where(p => p.DateRecord >= date1).Where(p => p.DateRecord <= date2).ToArray();
                    var toDelivery = db.Delivery.Where(p => p.Date >= date1).Where(p => p.Date <= date2).ToArray();

                    int countOfPurchase = (from p in db.Purchase
                                           where p.DateRecord >= date1 && p.DateRecord <= date2

                                           select new { p.Amount }).Sum(p => p.Amount);
                    int countOfDelivery = (
                                            from p in db.Delivery
                                            join r in db.Receipts on p.IdReciept equals r.IDreceipt
                                            join f in db.Furniture_Bills on r.IDbill equals f.IDbill
                                            where p.Date >= date1 && p.Date <= date2
                                            select new { f.Amount }).Sum(p => p.Amount);
                    if (countOfPurchase > 0 || countOfDelivery>0)
                    {
                        //int countOfPurchase = purchase.Count();//количество заказов
                        
                       
                        WordHelper helper = new WordHelper(@"..\..\WordTemplates\StorageReport.docx");
                       Dictionary<string, string> items = new Dictionary<string, string>
                        {
                            {"<dateReport>", DateTime.Now.ToString("dd.MM.yyyy") },
                            {"<date1>", date1.ToString("dd.MM.yyyy") },
                            {"<date2>", date2.ToString("dd.MM.yyyy") },
                            {"<purchaseCount>", countOfPurchase.ToString() },
                            {"<deliveryCount>", countOfDelivery.ToString() },

                            
                        };

                        helper.CreateDocument(items, "Storage");
                    }
                    else
                    {
                        MessageBox.Show("Данных за прошлую неделю нет");
                    }
                }
            });

        }

        public ICommand CreateReport { get; set; }
        public ObservableCollection<FurnitureItem> Furnitures { get => furnitures; set => furnitures = value; }
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

        public ICommand NavigateSellerCommand { get; }
        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }

    }
}
