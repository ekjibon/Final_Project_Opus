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
    public class DailyBillReceiveReportViewModel
    {
        public int? billReceiveId { get; set; }

        public DateTime? BillReceiveDate { get; set; }
        public string ProcessNo { get; set; }
        public int? supplierId { get; set; }
        public string supplierName { get; set; }

        public string InvoiceNo { get; set; }
        public string billStatus { get; set; }
        public decimal? GBamount { get; set; }
       
       
        public decimal? Vat { get; set; }
        
        public decimal? Total { get; set; }
       
        public int? BillStatusId { get; set; }
        public string action { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<DailyBillReceiveReportViewModel> dailyBillReceiveReportViewModels { get; set; }

    }
}
