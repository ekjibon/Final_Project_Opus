using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMIOU.Models
{
    public class IOUPaymentMasterViewModel
    {
        public int? iOUPaymentMasterId { get; set; }
        public string iouPaymentNo { get; set; }
        public DateTime? iouPaymentDate { get; set; }
        public string attentionTo { get; set; }
        public int? attentionToId { get; set; }
        public int iouPaymentStatus { get; set; }
        public string paymentBy { get; set; }
        public int? userId { get; set; }
        //public int? projectId { get; set; }
        public string remarks { get; set; }
        public int approveType { get; set; }



        public int?[] ioumasterId { get; set; }
        public int?[] projectId { get; set; }
        public string[] iouNo { get; set; }
        public string[] procurementPerson { get; set; }
        public decimal?[] iouValue { get; set; }
        public decimal[] subTotal { get; set; }
        

      

        public IEnumerable<RequisitionMaster> requisitionMasters { get; set; }
        public IEnumerable<IOUMaster> iOUMasters { get; set; }
        public IEnumerable<IOUMaster> issuedIOUMasters { get; set; }
        public IEnumerable<IOUPayment> iOUPayments { get; set; }
        public IEnumerable<Project> projects { get; set; }
        public IOUPayment iOUPayment { get; set; }
        public IEnumerable<IOUPayment> iOUAdjustments { get; set; }
        public IOUMaster iOUMaster { get; set; }
        public IEnumerable<IOUDetails> iOUDetails { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }
        public IEnumerable<RequisitionDetail> requisitionDetails { get; set; }
        public IEnumerable<RequisitionDetailViewModel> requisitionDetailViews { get; set; }
        public IEnumerable<DeliveryLocation> deliveryLocations { get; set; }
        public IEnumerable<Currency> currencies { get; set; }
        public ApproverPanelViewModel approverPanel { get; set; }
        public IEnumerable<IOUViewModel> iOUViewModels { get; set; }

        public IEnumerable<IOUPaymentMaster> iOUPaymentMasters { get; set; }
        public IEnumerable<IOUPaymentMaster> iOUPaymentMastersFinal { get; set; }
        public IEnumerable<IOUPaymentMaster> issuedIOUPaymentMasters { get; set; }
        public IOUPaymentMaster IOUPaymentMaster  { get; set; }

    }
}
