using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organization = OPUSERP.SCM.Data.Entity.Supplier.Organization;

namespace OPUSERP.Areas.Accounting.Models
{
    public class VendorViewModel
    {
        public int? groupId { get; set; }

        public int ledgerId { get; set; }
        public int vendorId { get; set; }

        public int subLedgerId { get; set; }
        //public string vendorCode { get; set; }

        //public string vendorName { get; set; }
        public string accountCode { get; set; }

        public string accountName { get; set; }
        public string aliasName { get; set; }

        public int? currencyId { get; set; }

        public int? haveSubLedger { get; set; }
        public int? specialBranchUnitId { get; set; }

        public int?[] ids { get; set; }

        public IEnumerable<Ledger> ledgers { get; set; }

        public IEnumerable<AccountGroup> accountGroups { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
    }
}
