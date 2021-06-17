using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Areas.OtherSales.Models.NotMapped;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models
{
    public class SalesCollectionViewModel
    {
        public int?[] selectPaymentHeadIds { get; set; }

        public int? relSupplierCustomerResourseId { get; set; }

        public decimal?[] selectPaymentHeadAmounts { get; set; }

        public decimal?[] selectPaymentHeadDues { get; set; }
        public int? storeId { get; set; }

        public decimal? total { get; set; }
        public decimal?bankAmount{ get; set; }
        public decimal? cashAmount { get; set; }
        public decimal? Amount { get; set; }
        public int? paymentModeId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        public string remarks { get; set; }
        public string bankName { get; set; }
        public string checqueNo { get; set; }
        public string trxNo { get; set; }
        public int? invoicemasterId { get; set; }
        public int? invoicemasterId2 { get; set; }

        public IEnumerable<PaymentInvoiceWithDue> paymentInvoiceWithDues { get; set; }

        public IEnumerable<CustomerWithDue> customerWithDues { get; set; }
        public IEnumerable<SalesReturnInvoiceMaster> salesReturnInvoiceMasters { get; set; }

        public IEnumerable<SalesCollection> salesCollections { get; set; }
        public IEnumerable<PosCollectionMaster> posSalesCollections { get; set; }

        public IEnumerable<SalesCollectionDetail> salesCollectionDetails { get; set; }
        public IEnumerable<BillReturnPaymentDetail> billReturnPaymentDetails { get; set; }

        public IEnumerable<RelSupplierCustomerResourse> relSupplierCustomerResourses { get; set; }
        public IEnumerable<SalesCollectionBillInfo> salesCollectionBillInfos { get; set; }
        public SalesCollectionBillInfo salesCollectionBillInfobyid { get; set; }

        public SalesCollection salesCollection { get; set; }
        public BillReturnPaymentMaster billReturnPaymentMaster { get; set; }
        public CustomerCollectionReportVM customerCollectionReportVM { get; set; }
        public IEnumerable<PaymentMode> paymentModes { get; set; }
        public IEnumerable<VoucherMaster> voucherMasters { get; set; }
        public IEnumerable<BankInfo> bankInfos { get; set; }
        public IEnumerable<MobileBankingType> mobileBankingTypes { get; set; }
        //public IEnumerable<StoreAssign> storeAssigns { get; set; }
        public Company company { get; set; }
        public IEnumerable<Company> companies { get; set; }

        public RelSupplierCustomerResourse relSupplierCustomerResourse { get; set; }

        public CollectionSummaryReport collectionSummaryReport { get; set; }


    }
}
