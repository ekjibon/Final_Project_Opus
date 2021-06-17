using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Budget.Models
{
    public class BudgetMainHeadViewModel
    {
        public int? headId { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string codeBN { get; set; }
        public string nameBN { get; set; }

        public BudgetMainHeadLn fLang { get; set; }
        public IEnumerable<BudgetMainHead> budgetMainHeads { get; set; }
    }
}
