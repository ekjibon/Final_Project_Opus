using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class BillReceiveViewModel
    {
        public decimal? invoiceAmount { get; set; }
        public string supplier { get; set; }
        public string invoiceNo { get; set; }
        public string processNo { get; set; }
        public DateTime? exInvPayDate { get; set; }
        public DateTime? exBillReFinDate { get; set; }
        public DateTime? invRecvDate { get; set; }
        public DateTime? receiveOnDate { get; set; }
        public string acknowledgeBy { get; set; }

        public DailyBillReceive dailyBillReceive { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public IEnumerable<DailyBillReceive> dailyBillReceives { get; set; }
        public AutoProcessNumberViewModel AutoProcessNumberViewModel { get; set; }
    }
}
