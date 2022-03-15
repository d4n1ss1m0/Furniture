using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Furniture")]
    public class Furniture
    {
        private int idFurniture;
        private string name;
        private string description;
        private int idSupplier;
        private string category;
        private string color;
        private int amount;
        private decimal price;
        private decimal purchasePrice;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDfurniture { get => idFurniture; set => idFurniture = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int IDsupplier { get => idSupplier; set => idSupplier = value; }
        public string Category { get => category; set => category = value; }
        public string Color { get => color; set => color = value; }
        public int Amount { get => amount; set => amount = value; }
        public decimal Price { get => price; set => price = value; }
        public decimal PurchasePrice { get => purchasePrice; set => purchasePrice = value; }
    }
}
