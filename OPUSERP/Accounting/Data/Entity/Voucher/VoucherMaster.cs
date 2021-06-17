using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("VoucherMaster", Schema = "ACCOUNT")]
    public class VoucherMaster : Base
    {
        [Column(TypeName = "nvarchar(150)")]
        public string voucherNo { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string refNo { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? voucherDate { get; set; }

        public int? voucherTypeId { get; set; }
        public virtual VoucherType voucherType { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string remarks { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? voucherAmount { get; set; }

        public int? isPosted { get; set; }

        public int? fiscalYearId { get; set; }
        public virtual SalaryYear fiscalYear { get; set; }

        public int? taxYearId { get; set; }
        public virtual TaxYear taxYear { get; set; }

        public int? companyId { get; set; }

        public int? fundSourceId { get; set; }
        public virtual FundSource fundSource { get; set; }

        //Optional Field
        [NotMapped]
        public int? ledgerRelationId { get; set; }

        [NotMapped]
        public string accountName { get; set; }

        [NotMapped]
        public decimal? amount { get; set; }
        public int? projectId { get; set; }
        public Project project { get; set; }


    }
}
