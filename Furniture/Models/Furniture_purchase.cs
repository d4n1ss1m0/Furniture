using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Furniture_purchase")]
    public class Furniture_purchase
    {
        private int idFurniture;
        private int idPurchase;
        private int amount;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDfurniture { get => idFurniture; set => idFurniture = value; }
        public int IDpurchase { get => idPurchase; set => idPurchase = value; }
        public int Amount { get => amount; set => amount = value; }
    }
}
