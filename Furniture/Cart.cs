using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture
{
    public class Cart
    {
        private Models.Furniture furniture;
        private int amountCart;
        private decimal cost;


        public Models.Furniture Furniture { get => furniture; set => furniture = value; }
        public int AmountCart { get => amountCart; set => amountCart = value; }
        public decimal Cost { get => cost; set => cost = value; }

        public Cart(Models.Furniture F, int A)
        {
            furniture = F;
            amountCart = A;
            Cost = furniture.Price * amountCart;
        }
    }
}
