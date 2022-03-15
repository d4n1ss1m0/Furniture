using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Supplier")]
    public class Supplier
    {
        private int idSupplier;
        private string name;
        private string address;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDsupplier { get => idSupplier; set => idSupplier = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
    }
}
