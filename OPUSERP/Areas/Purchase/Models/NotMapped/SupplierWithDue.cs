
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models.NotMapped
{
    public class SupplierWithDue
    {
        public Organization relSupplierCustomerResourse { get; set; }
        public decimal? haveToPay { get; set; }
        public decimal? paid { get; set; }
        public decimal? due { get; set; }
        public int? storeId { get; set; }
    }
}
