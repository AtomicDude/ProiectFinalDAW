using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ProiectFinalDAW.Models.Base;

namespace ProiectFinalDAW.Models
{
    public enum role
    {
        User, Admin
    }

    public class User : BaseEntity
    {

        public string Username { get; set; }

        public string Password { get; set; }

        public role Role { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email_Address { get; set; }

        public string Phone_Number { get; set; }

        public virtual FavouriteAddress FavouriteAddress { get; set; }

        public Guid FavouriteAddressId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
