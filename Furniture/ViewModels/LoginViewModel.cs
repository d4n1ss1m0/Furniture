using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using Furniture.Commands;
using Furniture.Store;
using System.Windows;

namespace Furniture.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public LoginViewModel(NavigationStore navigationStore)
        {
            LoginCommand = new SmartCommand(() =>
            {
                using (FurnitureContext db = new FurnitureContext())
                {
                    //авторизация
                    var login = new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@login",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Size = 50,
                        Value = Login
                    };
                    var password = new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@password",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Size = 50,
                        Value = Password
                    };
                    var result = new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@result",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Output,
                        Size = 250
                    };
                    db.Database.ExecuteSqlRaw("UserLogin @login, @password, @result OUTPUT", login, password, result);
                    if (result.Value.ToString() == "User successfully logged in")
                    {
                        var acc = db.Accounts.Where(a => a.login == login.Value.ToString()).FirstOrDefault();
                        App.acc = acc;
                        NavigationStore navigation_Store = new NavigationStore();
                        //проверка на роли
                        if (acc.role == "Seller")
                        {
                            new Windows.SellerInterface(navigation_Store).Show();
                        }
                        else if(acc.role == "Storage")
                        {
                            new Windows.StorageInterface(navigation_Store).Show();
                        }
                        App.Current.MainWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверно");
                    }

                };
            });
          
            /*
            NavigateFurnitureCommand = new NavigateCommand<FurnitureViewModel>(navigationStore, () => new FurnitureViewModel(navigationStore));
            NavigateSellerCommand = new NavigateCommand<SellerViewModel>(navigationStore, () => new SellerViewModel(navigationStore));
            NavigateAddFurnitureToOrderCommand = new NavigateCommand<AddFurnitureToOrderViewModel>(navigationStore, () => new AddFurnitureToOrderViewModel(navigationStore));*/
        }

        public ICommand LoginCommand { get; }

        public ICommand NavigateSellerCommand { get; }
        public ICommand NavigateFurnitureCommand { get; }
        public ICommand Kekw { get; }

    }
}
