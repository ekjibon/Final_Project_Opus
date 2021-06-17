using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("HOBudgetRequsitionMaster", Schema = "Budget")]
    public class HOBudgetRequsitionMaster : Base
    {
       
        public int? fiscalYearId { get; set; }
        public FiscalYear fiscalYear { get; set; }
        [MaxLength(150)]
        public string requsitionNo { get; set; }

        public DateTime? requsitionDate { get; set; }

        public int? RequsitionBy { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? grandTotal { get; set; }

        public int? status { get; set; }
    }
}
