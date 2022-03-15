using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Furniture_Bill")]
    public class Furniture_Bill
    {
        private int idBill;
        private int idFurniture;
        private int amount;

        [Key, Column(Order =0), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDbill { get => idBill; set => idBill = value; }
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDfurniture { get => idFurniture; set => idFurniture = value; }
        public int Amount { get => amount; set => amount = value; }
    }
}
