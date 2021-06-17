
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
    [Table("RepSalesInvoiceMaster", Schema = "Sales")]
    public class RepSalesInvoiceMaster: Base
    {
        public int? salesInvoiceMasterId { get; set; }
        public SalesInvoiceMaster salesInvoiceMaster { get; set; }
        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? invoiceDate { get; set; }
        public DateTime? paymentDate { get; set; }

        public string invoiceNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? totalAmount { get; set; }
        public decimal? VATOnTotal { get; set; }
        public decimal? TAXOnTotal { get; set; }
        public decimal? DiscountOnTotal { get; set; }
        public decimal? NetTotal { get; set; }

        public int? isClose { get; set; } //1=Paid, 0 = have Due
        public int? isStockClose { get; set; } //1 = full stock, 0 = Due
        //public int? storeId { get; set; }
        //public Store store { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string lcNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? lcDate { get; set; }

        //public int? countryId { get; set; }
        //public Country country { get; set; }

        //public int? salesType { get; set; }

    }
}
