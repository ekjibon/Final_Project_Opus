using OPUSERP.Accounting.Data.Entity.FDR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class FRDApprovalViewModel
    {
        public IEnumerable<FDRInvesmentMaster> fDRInvesmentMasters { get; set; }

        public IEnumerable<FDRInvestmentALLView> fDRInvestmentALLViews { get; set; }
    }
}
