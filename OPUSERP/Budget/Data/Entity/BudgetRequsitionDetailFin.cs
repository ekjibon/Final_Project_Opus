using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("BudgetRequsitionDetailFin", Schema = "Budget")]
    public class BudgetRequsitionDetailFin:Base
    {
        public int? budgetRequsitionMasterId { get; set; }
        public BudgetRequsitionMasterFin budgetRequsitionMaster { get; set; }

        public int? budgetHeadId { get; set; }
        public Ledger budgetHead { get; set; }
        public int? partnerId { get; set; }
        public Ledger partner { get; set; }

        public int? ledgerRelationId { get; set; }
        public LedgerRelation ledgerRelation { get; set; }

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
