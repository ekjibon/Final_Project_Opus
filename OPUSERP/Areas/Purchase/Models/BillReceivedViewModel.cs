using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models
{
    public class BillReceivedViewModel
    {
        public int? customerId { get; set; }
        public int? invoiceId { get; set; }
        public decimal? BillTotal { get; set; }
        public decimal? DueDate { get; set; }
        public DateTime? poDate { get; set; }
    }
}
