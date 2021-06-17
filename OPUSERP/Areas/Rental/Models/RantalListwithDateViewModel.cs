using OPUSERP.Rental.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Rental.Models
{
    public class RantalListwithDateViewModel
    {
        public RentInvoiceMaster rentInvoiceMaster { get; set; }
        public RentInvoiceDetail rentInvoiceDetail { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public int? dateDiff { get; set; }
    }
}
