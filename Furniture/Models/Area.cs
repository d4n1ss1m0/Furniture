using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture.Models
{
    public class Area
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDarea{ get; set; }
        public string Name { get; set; }
        public decimal CostDelivery { get; set; }
        public double ExpensesOil { get; set; }
        public double ExpensesFuel { get; set; }
    }
}
