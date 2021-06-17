using OPUSERP.Accounting.Data.Entity.FDR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class FDRInvestmentALLView
    {
        public FDRInvesmentMaster fDRInvesmentMaster { get; set; }
        public IEnumerable<FDRInvestmentDetail> fDRInvestmentDetails { get; set; }
    }
}
