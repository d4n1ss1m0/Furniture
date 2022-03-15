using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Trucks")]
    public class Truck
    {
        private int idTruck;
        private string brand;
        private string model;
        private string plateNum;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTruck { get => idTruck; set => idTruck = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Model { get => model; set => model = value; }
        public string PlateNum { get => plateNum; set => plateNum = value; }
    }
}
