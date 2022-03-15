using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture.Models
{
    public class Application
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDapplication { get; set; }
        public int IDfurniture { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        
    }
}
