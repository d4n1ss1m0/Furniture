using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture.Models
{
    public class Accounts
    {
        public int id { get; set; }
        public string login { get; set; }
        public byte[] passwordHash { get; set; }
        public string role { get; set; }
    }
}
