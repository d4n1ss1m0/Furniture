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
        private decimal sum;
        private DateTime datePurchase;
        private DateTime timePurchase;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDPurchase { get => idPurchase; set => idPurchase = value; }
        public decimal Sum { get => sum; set => sum = value; }
        public DateTime DatePurchase { get => datePurchase; set => datePurchase = value; }
        public DateTime TimePurchase { get => timePurchase; set => timePurchase = value; }
    }
}
