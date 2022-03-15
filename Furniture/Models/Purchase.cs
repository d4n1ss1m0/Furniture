using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    public class Purchase
    {
        private int idPurchase;
        private int idFurniture;
        private decimal sum;
        private int amount;
        private DateTime datePurchase;
        private DateTime? dateRecord;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDPurchase { get => idPurchase; set => idPurchase = value; }
        public decimal Sum { get => sum; set => sum = value; }
        public DateTime DatePurchase { get => datePurchase; set => datePurchase = value; }
        public DateTime? DateRecord { get => dateRecord; set => dateRecord = value; }
        public int IDfurniture { get => idFurniture; set => idFurniture = value; }
        public int Amount { get => amount; set => amount = value; }
    }
}
