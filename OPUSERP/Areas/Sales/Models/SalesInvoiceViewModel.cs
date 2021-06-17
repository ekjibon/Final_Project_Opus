
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Sales.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Sales.Models
{
    public class SalesInvoiceViewModel
    {
        public int? masterId { get; set; }
        public int? customerId { get; set; }
        public int? storeId { get; set; }
        public int?[] itemPriceFixingId { get; set; }
        public decimal?[] tblQuantity { get; set; }
        public decimal?[] lineTotal { get; set; }
        public decimal?[] linediscount { get; set; }
        public decimal?[] linetax { get; set; }
        public decimal?[] linevat { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? invoiceDate { get; set; }
        public DateTime? paymentDate { get; set; }

        public string salesInvoiceNo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? grossTotal { get; set; }
        public decimal? grossVAT { get; set; }
        public decimal? grossDiscount { get; set; }
        public decimal? netTotal { get; set; }

        //LC
        public string lcNo { get; set; }
        public DateTime? lcDate { get; set; }
        public int? countryId { get; set; }
        public Country country { get; set; }


        public IEnumerable<AgreementDetails> itemPriceFixings { get; set; }

        public IEnumerable<SalesInvoiceMaster> salesInvoiceMasters { get; set; }
        
        public SalesInvoiceMaster salesInvoiceMaster { get; set; }

        public IEnumerable<SalesInvoiceDetail> salesInvoiceDetails { get; set; }
      
        public Company company { get; set; }
        
    }
}
