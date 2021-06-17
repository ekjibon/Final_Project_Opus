using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class BudgetExpenseDetailPViewModel
    {

        public string budgetHeadName { get; set; }
        public string parnerName { get; set; }
   
        public decimal? FirstQ { get; set; }
        public decimal? FirstQP { get; set; }
        public decimal? SecondQ { get; set; }
        public decimal? SecondQP { get; set; }
        public decimal? thirdQ { get; set; }
        public decimal? thirdQP { get; set; }
        public decimal? fourthQ { get; set; }
        public decimal? fourthQP { get; set; }

    }
}
