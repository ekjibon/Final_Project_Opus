using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("SalesCollectionBillInfo", Schema = "OSales")]
    public class SalesCollectionBillInfo:Base
    {
        public int? relSupplierCustomerResourseId { get; set; }
        public RelSupplierCustomerResourse relSupplierCustomerResourse { get; set; }

        public int? salesInvoiceMasterId { get; set; }
        public SalesInvoiceMaster salesInvoiceMaster { get; set; }

        public string billPaymentNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? billPaymentDate { get; set; }

        public decimal? Amount { get; set; }

        public decimal? dueAmount { get; set; }

        public string remarks { get; set; }

        public int? ispaid { get; set; }
    }
}
