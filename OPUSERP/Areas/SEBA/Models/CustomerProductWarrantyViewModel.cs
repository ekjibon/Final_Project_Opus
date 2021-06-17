using OPUSERP.SEBA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SEBA.Models
{
    public class CustomerProductWarrantyViewModel
    {
        public int warrantyId { get; set; }
        public int? leadsId { get; set; }
        public int? itemSpecificationId { get; set; }  
        public DateTime? salesDate { get; set; }
        public DateTime? warrantyStartDate { get; set; }
        public DateTime? warrantyEndDate { get; set; }
        public string warrantyDescription { get; set; }       
        public string invoiceNo { get; set; }        
        public string serialNo { get; set; }       
        public string modelNo { get; set; }

        public IEnumerable<CustomerProductWarranty> customerProductWarranties { get; set; }
    }
}
