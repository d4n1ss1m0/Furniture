using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Category")]
    public class Category
    {
        private string _category;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string category { get => _category; set => _category = value; }
    }
}
