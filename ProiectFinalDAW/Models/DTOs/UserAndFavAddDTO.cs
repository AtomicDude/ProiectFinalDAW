using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectFinalDAW.Models.DTOs
{
    public class UserAndFavAddDTO
    {
        public string Username { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email_Address { get; set; }

        public string Phone_Number { get; set; }

        public string FavouriteAddress { get; set; }
    }
}
