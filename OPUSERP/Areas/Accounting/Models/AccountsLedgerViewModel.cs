using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class AccountsLedgerViewModel
    {
        public int? ledgerId { get; set; }
        public string ledgerName { get; set; }
        public string ledgerCode { get; set; }
        public int? groupId { get; set; }
        public DateTime? effectiveDate { get; set; }
    }
}
