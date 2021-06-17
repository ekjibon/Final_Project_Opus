using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("BudgetSubHead", Schema = "Budget")]
    public class BudgetSubHead:Base
    {
        public string code { get; set; }
        public string codeBN { get; set; }

        public string name { get; set; }
        public string nameBN { get; set; }

        public int? budgetMainHeadId { get; set; }
        public BudgetMainHead budgetMainHead { get; set; }

        public int? shortOrder { get; set; }
    }
}
