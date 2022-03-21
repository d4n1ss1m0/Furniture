using Furniture.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture.Models;

namespace Furniture.ViewModels
{
    public class AddApplicationViewModel:ViewModelBase
    {
        public AddApplicationViewModel()
        {
            Save = new SmartCommand(() => {
                using (FurnitureContext db = new FurnitureContext())
                {
                    Application application = new Application();
                    application.Amount = Amount;
                    application.IDfurniture = ID;
                    application.Status = "Ожидает";
                    application.Date = DateTime.Now;
                    application.IDapplication = db.Applications.AsEnumerable().Last().IDapplication+1;
                    db.Applications.Add(application);
                    db.SaveChanges();
                    ApplicationsViewModel.Applications.Add(application);
                    
                }
            });
        }

        public ICommand Save { get; set; }
        public int ID { get; set; }
        public int Amount { get; set; }
    }
}
