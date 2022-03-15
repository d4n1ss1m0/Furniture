using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furniture.Models
{
    [Table("Receipt")]
    public class Receipt
    {
        private int idReceipt;
        private TimeSpan receiveTime;
        private DateTime recieveDate;
        private string address;
        private int idBill;
        private string phone;
        private string firstName;
        private string lastName;
        private string middleName;
        private int idArea;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDreceipt { get => idReceipt; set => idReceipt = value; }
        public TimeSpan ReceiveTime { get => receiveTime; set => receiveTime = value; }
        public DateTime RecieveDate { get => recieveDate; set => recieveDate = value; }
        public string Address { get => address; set => address = value; }
        public int IDbill { get => idBill; set => idBill = value; }
        public string Phone { get => phone; set => phone = value; }
        public int IDarea { get => idArea; set => idArea = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
    }
}
