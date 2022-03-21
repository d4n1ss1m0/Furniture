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
    public class AreaViewModel : ViewModelBase
    {
        public ObservableCollection<Area> Areas { get; set; }
        public ICommand CreateReport { get; set; }
        public AreaViewModel(NavigationStore navigationStore)
        {
            Areas = new ObservableCollection<Area>();
            using (FurnitureContext db = new FurnitureContext())
            {
                foreach (Area area in db.Area)
                {
                    Areas.Add(area);
                }
            }
            CreateReport = new SmartCommand(() => {
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
                    var expen= (
                                            from p in db.Delivery
                                            join r in db.Receipts on p.IdReciept equals r.IDreceipt
                                            join a in db.Area on r.IDarea equals a.IDarea
                                            where p.Date >= date1 && p.Date <= date2                                            
                                            select new { a.ExpensesOil, a.ExpensesFuel }).ToArray();

                    if (expen.Length > 0)
                    {
                        double Oil = 0;
                        double Fuel = 0;
                        foreach(var i in expen)
                        {
                            Oil += i.ExpensesOil;
                            Fuel += i.ExpensesFuel;
                        }
                        WordHelper helper = new WordHelper(@"..\..\WordTemplates\ExpensesDelivery.docx");
                        Dictionary<string, string> items = new Dictionary<string, string>
                        {
                            {"<dateReport>", DateTime.Now.ToString("dd.MM.yyyy") },
                            {"<date1>", date1.ToString("dd.MM.yyyy") },
                            {"<date2>", date2.ToString("dd.MM.yyyy") },
                            {"<Oil>", Oil.ToString() },
                            {"<Fuel>", Fuel.ToString() },
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
