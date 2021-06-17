using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models
{
    public class PatientInvoiceDetailViewModel
    {
        public int Id { get; set; }
        public int itemId { get; set; }
        public int itemSpecificationId { get; set; }
        public string itemName { get; set; }
        public string specificationName { get; set; }      
        public decimal? billAmount { get; set; }       
    }
}
