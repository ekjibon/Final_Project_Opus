
using OPUSERP.Areas.Purchase.Models.NotMapped;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models
{
    public class BillPaymentViewModel
    {
        public int?[] selectPaymentHeadIds { get; set; }

        public int? relSupplierCustomerResourseId { get; set; }

        public decimal?[] selectPaymentHeadAmounts { get; set; }

        public decimal?[] selectPaymentHeadDues { get; set; }

        public decimal? total { get; set; }
        public decimal? bankAmount { get; set; }
        public decimal? cashAmount { get; set; }
        public int? paymentModeId { get; set; }
        public int? storeId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        [Display(Name = "Bill Payment Date")]
        public DateTime date { get; set; }

        public string remarks { get; set; }

        //public IEnumerable<PurchaseInvoiceWithDue> purchaseInvoiceWithDues { get; set; }
        //public IEnumerable<ReturnPayInvoiceWithDue> returnPayInvoiceWithDues { get; set; }

        public IEnumerable<SupplierWithDue> supplierWithDues { get; set; }


        public IEnumerable<PaymentMode> paymentModes { get; set; }
        public IEnumerable<Organization> relSupplierCustomerResourses { get; set; }

        //public IEnumerable<StoreAssign> storeAssigns { get; set; }
        public IEnumerable<Company> company { get; set; }

        public IEnumerable<BillPaymentMaster> billPaymentMasters { get; set; }

        public BillPaymentMaster billPaymentMaster { get; set; }
        public IEnumerable<BillPaymentDetail> billPaymentDetails { get; set; }
        public IEnumerable<BillReceiveInformation> billReceiveInformation { get; set; }
    }
}
