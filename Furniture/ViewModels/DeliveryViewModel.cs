 using Furniture.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture.Models;
using System.Data.Entity;
using System.Windows.Input;
using Furniture.Commands;
using System.Globalization;
using System.Windows;

namespace Furniture.ViewModels
{
    public class DeliveryViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private ObservableCollection<DeliveryItem> delivery;
        private DeliveryItem selectedItem;
        private bool startingToday;
        public bool StartingToday
        {
            get
            {
                return startingToday;
            }
            set
            {
                startingToday = value;
                Refrash();

            }
        }

        public ObservableCollection<DeliveryItem> Delivery { get => delivery; set => delivery = value; }
        public ICommand CreateReport { get; set; }
        public DeliveryItem SelectedItem { get => selectedItem; set => selectedItem = value; }

        public ICommand GetReciept { get; set; }

        public ICommand GetTask { get; set; }

        public DeliveryViewModel(NavigationStore navigationStore)
        {
            Delivery = new ObservableCollection<DeliveryItem>();
            StartingToday = false;
            CreateReport = new SmartCommand(() =>
            {
                using (FurnitureContext db = new FurnitureContext())
                {
                    //Первый день в году
                    DateTime startDate = DateTime.Parse("01.01." + DateTime.Now.ToString("yyyy"));
                    //Получаем номер предыдущей недели
                    int week = (new GregorianCalendar()).GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday) - 1;
                    //Даты недели
                    DateTime date1 = FirstDateOfWeek(Convert.ToInt32(DateTime.Now.ToString("yyyy")), week, CultureInfo.CurrentCulture);
                    DateTime date2 = date1.AddDays(6);
                    //Выборка квитанций, которые были оформлены на прошлой неделе
                    string reportItems = "";
                    int countReportItems = 0;
                    foreach (DeliveryItem d in Delivery)
                    {
                        if (d.delivery.Date >= date1 && d.delivery.Date <= date2)
                        {

                            reportItems += d.delivery.Date.ToShortDateString() + " " + d.delivery.Time.ToString() + " - " + d.order + " по адресу: " + d.receipt.Address + "\n\r";
                            countReportItems++;
                        }
                    }

                    if (countReportItems > 0)
                    {
                        //int countOfPurchase = purchase.Count();//количество заказов


                        WordHelper helper = new WordHelper(@"..\..\WordTemplates\DeliveryReport.docx");
                        Dictionary<string, string> items = new Dictionary<string, string>
                        {
                            {"<dateReport>", DateTime.Now.ToString("dd.MM.yyyy") },
                            {"<date1>", date1.ToString("dd.MM.yyyy") },
                            {"<date2>", date2.ToString("dd.MM.yyyy") },
                            {"<deliveryCount>", countReportItems.ToString() },
                            {"<Items>", reportItems },


                        };

                        helper.CreateDocument(items, "Storage");
                    }
                    else
                    {
                        MessageBox.Show("Данных за прошлую неделю нет");
                    }
                }
            });
            GetReciept = new SmartCommand(() =>
            {
                using (FurnitureContext db = new FurnitureContext())
                {


                    var name = db.Area.Where(p => p.IDarea == SelectedItem.receipt.IDarea).First();
                    WordHelper helper = new WordHelper(@"..\..\WordTemplates\RecieptDelivery.docx");
                    Dictionary<string, string> items = new Dictionary<string, string>
                    {
                        {"<IDreciept>", SelectedItem.receipt.IDreceipt.ToString() },
                        {"<FIO>", SelectedItem.receipt.FirstName.ToString().TrimEnd(' ') +" "+SelectedItem.receipt.LastName.ToString().TrimEnd(' ') +" "+SelectedItem.receipt.MiddleName.ToString().TrimEnd(' ') +" "  },
                        {"<Address>", SelectedItem.receipt.Address.TrimEnd(' ')},
                        {"<Area>", db.Area.Where(p=>p.IDarea == SelectedItem.receipt.IDarea).First().Name},
                        {"<Order>", SelectedItem.order },
                        {"<Date>", SelectedItem.delivery.Date.ToShortDateString() },
                        {"<Time>", SelectedItem.delivery.Time.ToString() },

                    };

                    helper.CreateDocument(items, "Storage");
                }
            });
            GetTask = new SmartCommand(() =>
            {
                using (FurnitureContext db = new FurnitureContext())
                {
                    var Del = db.Deliveries.ToList();
                    string tasks = "";
                    foreach(Delivery del in Del)
                    {
                        if(del.Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                        tasks += del.Time + " - " + db.Receipts.AsEnumerable().Where(p => p.IDreceipt == del.IdReciept).Last().Address.TrimEnd(' ') + " Квитанция №" + del.IdReciept+"\n\r"; 
                    }
                    WordHelper helper = new WordHelper(@"..\..\WordTemplates\DeliveryTask.docx");
                    Dictionary<string, string> items = new Dictionary<string, string>
                    {
                        {"<Items>", tasks },
                        {"<dateReport>", DateTime.Now.ToShortDateString()  },                       
                    };
                    helper.CreateDocument(items, "Tasks");
                }
            });
        }

        public void Refrash()
        {
            using (FurnitureContext db = new FurnitureContext())
            {
                System.Linq.IQueryable querry;
                if (startingToday)
                {
                    querry = db.Delivery.Where(p => p.Date >= DateTime.Now);
                }
                else
                {
                    querry = db.Delivery;
                }
                foreach(Delivery d in querry)
                {
                    Delivery.Add(new DeliveryItem(d));
                }
                
            }
        }

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
