using OPUSERP.REMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.REMS.Models
{
    public class ClaimReportsViewModel
    {
        public string claimByName { get; set; }

        public ClaimRegister claimRegister { get; set; }
    }
}
