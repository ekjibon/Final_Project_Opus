using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Data.Entity.Requisition
{
    [Table("RequisitionMaster", Schema = "Distribution")]
    public class RequisitionMaster : Base
    {
        public int? relSupplierCustomerResourseId { get; set; }
        public RelSupplierCustomerResourse relSupplierCustomerResourse { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? requisitionDate { get; set; }
       

        public string requisitionNumber { get; set; }

        public decimal? totalAmount { get; set; }
        public decimal? VATOnTotal { get; set; }
        public decimal? TAXOnTotal { get; set; }
        public decimal? DiscountOnTotal { get; set; }
        public decimal? NetTotal { get; set; }

        public decimal? apptotalAmount { get; set; }
        public decimal? appVATOnTotal { get; set; }
        public decimal? appTAXOnTotal { get; set; }
        public decimal? appDiscountOnTotal { get; set; }
        public decimal? appNetTotal { get; set; }

        public int? isApproved { get; set; } //1=Approved, 0 = not Approved
        
    }
}
