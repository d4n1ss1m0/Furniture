using Furniture.Commands;
using Furniture.Models;
using Furniture.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Furniture.ViewModels
{
    public class ApplicationsBookViewModel:ViewModelBase
    {
        public ICommand CreatePurchase { get; set; }
        public static ICommand Update { get; set; }
        private ObservableCollection<Application> applications;
        public Application SelectedApplication { get; set; }
        public ApplicationsBookViewModel(NavigationStore navigationStore)
        {           
            Update = new SmartCommand(() => {
                Applications = new ObservableCollection<Models.Application>();
                using (FurnitureContext db = new FurnitureContext())
                {
                    foreach (Application application in db.Applications.Where(p => p.Status == "Ожидает"))
                    {
                        //Console.WriteLine("{0}.{1} - {2}", seller.IDseller, seller.FirstName, seller.LastName);
                        Applications.Add(application);
                    }
                }
                Console.WriteLine("Сукааа");
            });
            Update.Execute("update");
            CreatePurchase = new SmartCommand(() => {
                new Windows.AddPurchaseView(SelectedApplication).Show();       
            });
        }
        public ObservableCollection<Application> Applications
        {
            get
            {
                return applications;
            }
            set
            {
                applications = value;
                OnPropertyChanged("Applications");
            }
        }
    }
}
