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

        [Key, Column(Order = 0)]
        public int IDbill { get => idBill; set => idBill = value; }
        [Key, Column(Order = 1)]
        public int IDfurniture { get => idFurniture; set => idFurniture = value; }
        public int Amount { get => amount; set => amount = value; }


    }
}
