using OPUSERP.Payroll.Data.Entity.Salary;
using System.Collections.Generic;

namespace OPUSERP.Areas.Payroll.Models
{
    public class WalletTypeViewModel
    {
        public int walletTypeId { get; set; }
        public string walletTypeName { get; set; }

        public IEnumerable<WalletType> walletTypes { get; set; }
    }
}
