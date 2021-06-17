
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
    [Table("RepSalesInvoiceDetail", Schema = "Sales")]
    public class RepSalesInvoiceDetail: Base
    {
        public int? agreementDetailsId { get; set; }
        public AgreementDetails agreementDetails { get; set; }

        public int? repSalesInvoiceMasterId { get; set; }
        public RepSalesInvoiceMaster repSalesInvoiceMaster { get; set; }
        public int? salesInvoiceDetailId { get; set; }
        public SalesInvoiceDetail salesInvoiceDetail { get; set; }
        public decimal? quantity { get; set; }

        public decimal? unitPrice { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? discountAmount { get; set; }

    }
}
