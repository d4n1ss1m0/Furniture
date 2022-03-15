using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    class Loaders
    {
        private int idLoader;
        private string firstName;
        private string lastName;
        private string midName;
        private string phone;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDloader { get => idLoader; set => idLoader = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MidlleName { get => midName; set => midName = value; }
        public string Phone { get => phone; set => phone = value; }
    }
}
