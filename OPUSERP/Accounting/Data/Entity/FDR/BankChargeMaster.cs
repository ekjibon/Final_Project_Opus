using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Data.Entity.FDR
{
    [Table("BankChargeMaster", Schema = "ACCOUNT")]
    public class BankChargeMaster:Base
    {
        public int? bankBranchDetailsId { get; set; }
        public BankBranchDetails bankBranchDetails { get; set; }
        [MaxLength(300)]
        public string AccountName { get; set; }
        [MaxLength(30)]
        public string AccountNumber { get; set; }

        public int? ledgerRelationId { get; set; }
        public LedgerRelation ledgerRelation { get; set; }
        [MaxLength(10)]
        public string Status { get; set; }
        [MaxLength(10)]
        public string AccountType { get; set; }
    }
}
