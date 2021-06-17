using OPUSERP.CRM.Data.Entity.Budget;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class TeamBudgetViewModel
    {
        public int? teamBudegtId { get; set; }
        public int? teamId { get; set; }
        public int[] teamIds { get; set; }
        public int? companyBudgetsId { get; set; }
        public decimal? initialAmount { get; set; }
        public decimal? surveillence { get; set; }
        public decimal[] initialAmounts { get; set; }
        public decimal[] surveillences { get; set; }
        public int? yearId { get; set; }
        public IEnumerable<Year> years { get; set; }
        public IEnumerable<TeamBudgets> teamBudgets { get; set; }
    }
}
