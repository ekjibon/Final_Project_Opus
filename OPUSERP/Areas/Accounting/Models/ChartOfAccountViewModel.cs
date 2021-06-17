using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class ChartOfAccountViewModel
    {
        public IEnumerable<AccountNatureViewModel> accountNatureViewModels { get; set; }
        public IEnumerable<AccountsGroupViewModel> accountsGroupViewModels { get; set; }
        public IEnumerable<AccountsLedgerViewModel> accountsLedgerViewModels { get; set; }
        //public string accountName { get; set; }
        //public string accountCode { get; set; }
        //public string accountGroup { get; set; }
        //public string natureName { get; set; }
        public IEnumerable<Company> companies { get; set; }
    }
}
