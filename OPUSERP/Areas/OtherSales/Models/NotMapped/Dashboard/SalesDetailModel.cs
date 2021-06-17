using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models.NotMapped.Dashboard
{
    public class SalesDetailModel
    {
        public string invoiceNumber { get; set; }
        public string invoiceDate { get; set; }
        public string  customerName { get; set; }
        public decimal? netAmount { get; set; }
        
    }
}
