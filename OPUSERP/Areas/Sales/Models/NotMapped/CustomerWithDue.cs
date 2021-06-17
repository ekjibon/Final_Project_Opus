
using OPUSERP.CRM.Data.Entity.Lead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Sales.Models.NotMapped
{
    public class CustomerWithDue
    {
        public Leads relSupplierCustomerResourse { get; set; }
        public decimal? haveToPay { get; set; }
        public decimal? paid { get; set; }
        public decimal? due { get; set; }
        public int? storeId { get; set; }
    }
}
