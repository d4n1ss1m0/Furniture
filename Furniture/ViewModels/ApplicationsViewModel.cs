using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Commands;
using Furniture.Store;
using Furniture.Models;

namespace Furniture.ViewModels
{
    public class ApplicationsViewModel : ViewModelBase
    {
        private static ObservableCollection<Application> applications;

        public ApplicationsViewModel(NavigationStore navigationStore)
        {
            Applications = new ObservableCollection<Models.Application>();
            using (FurnitureContext db = new FurnitureContext())
            {
                foreach (Application application in db.Applications)
                {
                    //Console.WriteLine("{0}.{1} - {2}", seller.IDseller, seller.FirstName, seller.LastName);
                    Applications.Add(application);
                }
            }
            //NavigateSellerCommand = new NavigateCommand<SellerViewModel>(navigationStore, () => new SellerViewModel(navigationStore));
            Add = new SmartCommand(() => {
                new Windows.AddApplicationView().Show();
            });
        }

        public ICommand Add { get; set; }
        public static ObservableCollection<Application> Applications { get => applications; set => applications = value; }
    }
}
