using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Budget.Models
{
    public class HQBudgetApprovalViewModel
    {

        public IEnumerable<BudgetRequsitionMaster> unitOfTakas { get; set; }
    }
}
