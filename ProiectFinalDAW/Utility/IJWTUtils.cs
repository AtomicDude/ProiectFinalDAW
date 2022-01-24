using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models;

namespace ProiectFinalDAW.Utility
{
    public interface IJWTutils
    {
        public string GenerateToken(User user);
        public Guid ValidateToken(string token);
    }
}
