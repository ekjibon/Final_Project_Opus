using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("BudgetRequsitionDetail", Schema = "Budget")]
    public class BudgetRequsitionDetail:Base
    {
        public int? budgetRequsitionMasterId { get; set; }
        public BudgetRequsitionMaster budgetRequsitionMaster { get; set; }

        public int? budgetHeadId { get; set; }
        public BudgetHead budgetHead { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? firstMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? secondMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? thirdMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? fourthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? fifthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? sixthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? seventhMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? eighthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ninethMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? tenthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? eleventhMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? twelvethMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? subTotal { get; set; }
        
    }
}
