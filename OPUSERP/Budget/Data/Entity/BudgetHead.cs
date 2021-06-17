using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("BudgetHead", Schema = "Budget")]
    public class BudgetHead:Base
    {
        public string code { get; set; }
        public string codeBN { get; set; }

        public string name { get; set; }      
        public string nameBN { get; set; }

        public int? budgetSubHeadId { get; set; }
        public BudgetSubHead budgetSubHead { get; set; }
        public int? shortOrder { get; set; }
    }
}
