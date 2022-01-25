using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models.Base;

namespace ProiectFinalDAW.Models
{
    static class Order_Counter
    {
        public static int Order_Number = 0;
    }

    public enum Order_Status 
    { 
        Received, Confirmed, Processing, Shipped
    }

    public class Order : BaseEntity
    {
        public int Order_Number { get; set; }

        public string Email_address { get; set; }

        public string Phone_number { get; set; }

        public string Address { get; set; }

        public Order_Status Status { get; set; }

        public virtual User User { get; set; }
        
        public Guid UserId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
