using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMPurchaseProcess.Models;
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
    public class IOUViewModel
    {
        public DateTime? expectedDeliveryDate { get; set; }
        public int?[] requisitionId { get; set; }
        public string attentionTo { get; set; }
        
        public int masterIdIOU { get; set; }
        public int projectId { get; set; }
        public string Remark { get; set; }
        public int approveType { get; set; }


        public decimal?[] rate { get; set; }
        public decimal?[] qty { get; set; }
        public decimal?[] vat { get; set; }
        public int[] detailsId { get; set; }
        public int[] deleteDetailListId { get; set; }

        public int?[] reqDetailsall { get; set; }
        public int?[] currencyall { get; set; }
        public int?[] txtLocationall { get; set; }
        public decimal?[] currencyRateall { get; set; }
        public decimal?[] txtVatall { get; set; }
        public decimal?[] poQntall { get; set; }
        public decimal?[] txtUnitRateall { get; set; }
        public decimal?[] txtAmtExVatall { get; set; }
        public decimal?[] txtVatAmountall { get; set; }
        public decimal?[] txtAitall { get; set; }
        public decimal?[] txtAitAmountall { get; set; }
        public string[] txtDescriptionall { get; set; }
        public string[] txtOtherLocationall { get; set; }

        public string[] iouNOAll { get; set; }
        public string disbarseNo { get; set; }
        public DateTime disbarseDate { get; set; }

        public string[] reqIdAll { get; set; }

        public IEnumerable<RequisitionMaster> requisitionMasters { get; set; }
        public IEnumerable<RFQReqDetail> rFQReqDetails { get; set; }
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
        public IEnumerable<RequisitionForCSViewModel> requisitionForCSViewModels { get; set; }
    }
}
