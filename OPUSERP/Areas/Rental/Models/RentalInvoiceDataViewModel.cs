using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Rental.Models
{
    public class RentalInvoiceDataViewModel
    {
        public string invoiceNumber { get; set; }
        public string leadName { get; set; }
        public int? relSupplierCustomerResourseId { get; set; }
    }
}
