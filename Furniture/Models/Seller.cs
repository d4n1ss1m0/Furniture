using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture.Models
{
    [Table("Sellers")]
    public class Seller
    {
        private int idSeller;
        private string firstName;
        private string lastName;
        private string midName;
        private decimal salary;
        private decimal sumProd;
        private int account;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDseller { get => idSeller; set => idSeller = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get =>  lastName; set => lastName = value; }
        public string MidName { get => midName; set => midName = value; }
        public decimal Salary { get => salary; set => salary = value; }
        public decimal SumProd { get => sumProd; set => sumProd = value; }
        public int Account { get => account; set => account = value; }
    }
}
