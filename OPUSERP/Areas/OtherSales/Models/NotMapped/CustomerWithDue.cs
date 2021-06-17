using OPUSERP.OtherSales.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models.NotMapped
{
    public class CustomerWithDue
    {
        public RelSupplierCustomerResourse relSupplierCustomerResourse { get; set; }
        public decimal? haveToPay { get; set; }
        public decimal? paid { get; set; }
        public decimal? due { get; set; }
        public int? storeId { get; set; }
    }
}
