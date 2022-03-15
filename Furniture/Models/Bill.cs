using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Bills")]
    public class Bill
    {
        private int idBill;
        private int idSeller;
        private decimal sum;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDbill { get => idBill; set => idBill = value; }
        public int IDSeller { get => idSeller; set => idSeller = value; }
        public decimal Sum { get => sum; set => sum = value; }
    }
}
