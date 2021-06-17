using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Budget.Data.Entity;
using System.Collections.Generic;

namespace OPUSERP.Areas.Budget.Models
{
    public class BudgetHeadViewModel
    {
        public int? headId { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public int? budgetSubHeadId { get; set; }

        public string codeBN { get; set; }
        public string nameBN { get; set; }
        public int?[] ids { get; set; }

        public BudgetHeadLn fLang1 { get; set; }
        public IEnumerable<BudgetHead> budgetHeads { get; set; }
        public IEnumerable<BudgetSubHead> budgetSubHeads { get; set; }
        public IEnumerable<BudgetMainHead> budgetMainHeads { get; set; }
    }
}
