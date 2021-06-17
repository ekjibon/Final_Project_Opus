//using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class LedgerDetailsViewModel
    {      
        public string accountName { get; set; }
        public string accountCode { get; set; }
        public decimal? Amount { get; set; }

        public Company company { get; set; }
        public IEnumerable<Company> companies { get; set; }
    }
}
