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
    public class CCWiseLedgerReportViewModel
    {
        public int? vmId { get; set; }
        public int? costcentreId { get; set; }
        public string vDate { get; set; }
        public DateTime? voucherDate { get; set; }
        public string voucherNo { get; set; }
        
        public string accountCode { get; set; }
        public string particular { get; set; }
        
        public string centreName { get; set; }
        
        public decimal? debit { get; set; }

        public decimal? credit { get; set; }
        
        public string action { get; set; }

     
       
    }
}
