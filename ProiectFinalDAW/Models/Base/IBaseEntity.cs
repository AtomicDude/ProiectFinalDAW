using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectFinalDAW.Models.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        DateTime? DataCreated { get; set; }

        DateTime? DateModified { get; set; }
    }
}
