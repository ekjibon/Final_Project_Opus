using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    public class BillReturnPaymentDetail : Base
    {
        public int? billReturnPaymentMasterId { get; set; }
        public BillReturnPaymentMaster billReturnPaymentMaster { get; set; }

        public int? salesReturnInvoiceMasterId { get; set; }
        public SalesReturnInvoiceMaster salesReturnInvoiceMaster { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

    }
}
