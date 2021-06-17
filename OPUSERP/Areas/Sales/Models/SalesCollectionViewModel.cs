

using OPUSERP.Areas.Sales.Models.NotMapped;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Master;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Sales.Data.Entity.Collection;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Sales.Models
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
        public int? paymentModeId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        public string remarks { get; set; }

        public IEnumerable<PaymentInvoiceWithDue> paymentInvoiceWithDues { get; set; }

        public IEnumerable<CustomerWithDue> customerWithDues { get; set; }

        public IEnumerable<SalesCollection> salesCollections { get; set; }
       

        public IEnumerable<SalesCollectionDetail> salesCollectionDetails { get; set; }
       

        public IEnumerable<Leads> relSupplierCustomerResourses { get; set; }

        public SalesCollection salesCollection { get; set; }
       
        public CustomerCollectionReportVM customerCollectionReportVM { get; set; }
        public IEnumerable<PaymentMode> paymentModes { get; set; }
       
        public IEnumerable<BankBranchDetails> bankInfos { get; set; }
        public IEnumerable<MobileBankingType> mobileBankingTypes { get; set; }
        //public IEnumerable<StoreAssign> storeAssigns { get; set; }
        public Company company { get; set; }


    }
}
