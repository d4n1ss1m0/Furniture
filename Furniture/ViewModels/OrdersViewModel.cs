using Furniture.Commands;
using Furniture.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Models;
using System.Windows;

namespace Furniture.ViewModels
{
    class OrdersViewModel:ViewModelBase
    {
        public ICommand CreateReport { get; set; }
        public ObservableCollection<OrdersItem> Orders { get; set; }
        public OrdersViewModel(NavigationStore navigationStore)
        {
            Orders = new ObservableCollection<OrdersItem>();
            using (FurnitureContext db = new FurnitureContext())
            {
                foreach (Models.Bill bill in db.Bills)
                {
                    Orders.Add(new OrdersItem(bill));
                }
            }
            CreateReport = new SmartCommand(() =>
            {
                using (FurnitureContext db = new FurnitureContext())
                {
                    //Первый день в году
                    DateTime startDate = DateTime.Parse("01.01." + DateTime.Now.ToString("yyyy"));
                    //Получаем номер предыдущей недели
                    int week = (new GregorianCalendar()).GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday)-1 ;
                    //Даты недели
                    DateTime date1 = FirstDateOfWeek(Convert.ToInt32(DateTime.Now.ToString("yyyy")), week, CultureInfo.CurrentCulture);                    
                    DateTime date2 = date1.AddDays(6);
                    //Выборка квитанций, которые были оформлены на прошлой неделе
                    var receipts = db.Receipts.Where(p => p.RecieveDate >= date1).Where(p => p.RecieveDate <= date2).ToArray();
                    if (receipts.Length > 0)
                    {
                        int countOfOrders = receipts.Count();//количество заказов
                        var furniture_Bills = (from f in db.Furniture_Bills
                                               join receipt in db.Receipts on f.IDbill equals receipt.IDbill
                                               where receipt.RecieveDate >= date1 && receipt.RecieveDate <= date2
                                               group f by f.IDfurniture into g
                                               select new { IDfuniture = g.Key, Sum = g.Sum(f => f.Amount) }).ToArray();
                        decimal ordersSum = (from b in db.Bills
                                             join receipt in db.Receipts on b.IDbill equals receipt.IDbill
                                             where receipt.RecieveDate >= date1 && receipt.RecieveDate <= date2
                                             select b.Sum).Sum();
                        int minCount = 0;
                        int maxCount = 0;
                        for (int i = 1; i < furniture_Bills.Count(); i++)
                        {
                            if (furniture_Bills[minCount].Sum > furniture_Bills[i].Sum)
                            {
                                minCount = i;
                            }
                            else if (furniture_Bills[maxCount].Sum < furniture_Bills[i].Sum)
                            {
                                maxCount = i;
                            }
                        }

                        int IDmaxCountF = furniture_Bills[maxCount].IDfuniture;
                        int IDminCountF = furniture_Bills[minCount].IDfuniture;
                        Seller seller = db.Sellers.Where(p=>p.Account == App.acc.id).First();
                        WordHelper helper = new WordHelper(@"..\..\WordTemplates\SellerReport.docx");
                        Dictionary<string, string> items = new Dictionary<string, string>
                        {
                            {"<dateReport>", DateTime.Now.ToString("dd.MM.yyyy") },
                            {"<date1>", date1.ToString("dd.MM.yyyy") },
                            {"<date2>", date2.ToString("dd.MM.yyyy") },
                            {"<ordersCount>", countOfOrders.ToString() },
                            {"<ordersSum>", ordersSum.ToString() },
                            {"<sLN>", seller.LastName },
                            {"<sFN>", seller.FirstName[0].ToString() },
                            {"<sMN>", seller.MidName[0].ToString() },
                            {"<maxCountF>", db.Furnitures.Where(p=>p.IDfurniture == IDmaxCountF).FirstOrDefault().Name.ToString().TrimEnd(' ')},
                            {"<maxCountC>", furniture_Bills[maxCount].Sum.ToString().Replace(" ",string.Empty) },
                            {"<minCountF>", db.Furnitures.Where(p=>p.IDfurniture == IDminCountF).FirstOrDefault().Name.ToString().TrimEnd(' ') },
                            {"<minCountC>", furniture_Bills[minCount].Sum.ToString().TrimEnd(' ')},
                        };

                        helper.CreateDocument(items, "Seller");
                    }
                    else
                    {
                        MessageBox.Show("Заказов за прошлую неделю нет");
                    }
                }
            });

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
