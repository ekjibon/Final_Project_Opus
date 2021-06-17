using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.CRM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class BankChargeMasterViewModel
    {
        public int Id { get; set; }

        public int? bankBranchDetailsId { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public int? ledgerRelationId { get; set; }

        public string Status { get; set; }

        public string AccountType { get; set; }

        public IEnumerable<Bank> banks { get; set; }
        public IEnumerable<BankBranchDetails> bankBranchDetails { get; set; }
        public IEnumerable<LedgerRelation> ledgerRelations { get; set; }
        public IEnumerable<BankChargeMaster> bankChargeMasters { get; set; }
    }
}
