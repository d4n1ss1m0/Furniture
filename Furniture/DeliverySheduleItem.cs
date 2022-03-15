using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture
{
    public class DeliverySheduleItem
    {
        private DateTime date;
        private DateTime time;
        private bool isAvalible;

        public string Date { get => date.ToShortDateString(); set => date = DateTime.Parse(value); }
        public string Time { get => time.ToShortTimeString(); set => time = DateTime.Parse(value); }
        public bool IsAvalible { get => isAvalible; set => isAvalible = value; }

        public DeliverySheduleItem(string d, string t, bool a)
        {
            Date = d;
            Time = t; 
            IsAvalible = a;
        }
    }
}
