using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Brigade")]
    public class Brigade
    {
        private int idBrigade;
        private int idDriver;
        private int idLoader;
        private int idTruck;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdBrigade { get => idBrigade; set => idBrigade = value; }
        public int IdDriver { get => idDriver; set => idDriver = value; }
        public int IdLoader { get => idLoader; set => idLoader = value; }
        public int IdTruck { get => idTruck; set => idTruck = value; }
    }
}
