using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture.Models;

namespace Furniture
{
    public class OrdersItem
    {
        private Bill bill;
        private Receipt receipt;
        private Seller seller;
        private string order;
        public OrdersItem(Bill bill)
        {
            using (FurnitureContext db = new FurnitureContext())
            {
                order = "";
                this.Bill = bill;
                Receipt = db.Receipts.Where(p => p.IDbill == bill.IDbill).First();
                Seller = db.Sellers.Where(p => p.IDseller == bill.IDSeller).First();
                var fBills = db.Furniture_Bills.Where(p => p.IDbill == bill.IDbill);
                foreach(Furniture_Bill furniture_Bill in fBills)
                {
                    Order += furniture_Bill.IDfurniture.ToString().TrimEnd(' ') + "(" + furniture_Bill.Amount.ToString().TrimEnd(' ') + " шт.) ";
                }
            }
        }

        public Bill Bill { get => bill; set => bill = value; }
        public Receipt Receipt { get => receipt; set => receipt = value; }
        public Seller Seller { get => seller; set => seller = value; }
        public string Order { get => order; set => order = value; }
    }
}
