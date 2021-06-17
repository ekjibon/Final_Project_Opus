
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Data.Entity.Sales
{
    [Table("SalesInvoiceDetail", Schema = "Sales")]
    public class SalesInvoiceDetail:Base
    {
        public int? agreementDetailsId { get; set; }
        public AgreementDetails agreementDetails { get; set; }

        public int? salesInvoiceMasterId { get; set; }
        public SalesInvoiceMaster salesInvoiceMaster { get; set; }
        public decimal? quantity { get; set; }

        public decimal? unitPrice { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? discountAmount { get; set; }

    }
}
