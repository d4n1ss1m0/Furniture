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
    public class AddUserViewModel
    {
        public string Login { get; set; }
        public string Password {get;set;}
        public string PasswordCheck { get; set; }
        public string Role { get; set; }
        public ICommand Add { get; set; }
        public AddUserViewModel()
        {
            Add = new SmartCommand(() => {
                using (FurnitureContext db = new FurnitureContext())
                {
                    if (Password == PasswordCheck)
                    {
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
                        var role = new Microsoft.Data.SqlClient.SqlParameter
                        {
                            ParameterName = "@role",
                            SqlDbType = System.Data.SqlDbType.VarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Size = 50,
                            Value = Role
                        };
                        var result = new Microsoft.Data.SqlClient.SqlParameter
                        {
                            ParameterName = "@result",
                            SqlDbType = System.Data.SqlDbType.VarChar,
                            Direction = System.Data.ParameterDirection.Output,
                            Size = 250
                        };
                        db.Database.ExecuteSqlRaw("AddAccount @login, @password,@role, @result OUTPUT", login, password,role, result);
                    }
                }
            });
        }
    }
}
