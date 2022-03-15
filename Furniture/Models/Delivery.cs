using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Delivery")]
    public class Delivery
    {
        private int idDelivery;
        private DateTime date;
        private TimeSpan time;
        private int idBrigade;
        private int idReciept;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDelivery { get => idDelivery; set => idDelivery = value; }
        public DateTime Date { get => date; set => date = value; }
        public TimeSpan Time { get => time; set => time = value; }
        public int IdBrigade { get => idBrigade; set => idBrigade = value; }
        public int IdReciept { get => idReciept; set => idReciept = value; }
    }
}
