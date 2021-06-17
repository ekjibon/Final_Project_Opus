using OPUSERP.Areas.SCMIOU.Models;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Stock;
using OPUSERP.SCM.Data.Entity.Supplier;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMReport.Models
{
    public class ReportMasterViewModel
    {
        public int? isCopy { get; set; }
        public string iouOwner { get; set; }
        public IEnumerable<Project> projects { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public Organization organization { get; set; }
        public RFQMaster RFQMaster { get; set; }
        public IEnumerable<RFQReqDetail> rFQReqDetails { get; set; }
        public IEnumerable<RFQReqDetailPrice> rFQReqDetailPrices { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<RequisitionMaster> requisitionMaster { get; set; }
        public IEnumerable<RequisitionDetail> requisitionDetailsList { get; set; }
        public IEnumerable<PurchaseOrderRPTViewModel> purchaseOrderRPTViewModels { get; set; }
        public IEnumerable<IOUDetails> iOUDetails { get; set; }
        public IEnumerable<IOUPaymentStatusVM> iOUPaymentStatusVMs { get; set; }
        public IEnumerable<StockDetails> stockDetails { get; set; }
        public IEnumerable<IOUPayment> iOUPayments { get; set; }
        public IEnumerable<Item> items { get; set; }
        public IEnumerable<PurchaseOrderSuppReportViewModel> purchaseOrderSuppReportViewModels { get; set; }        
        public IEnumerable<PurchaseOrderItemReportViewModel> purchaseOrderItemReportViewModels { get; set; }
        public IEnumerable<PurchaseOrderProjectReportViewModel> purchaseOrderProjectReportViewModels { get; set; }
        public IEnumerable<PurchaseOrderDiffReportViewModel> purchaseOrderDiffReportViewModels { get; set; }
        public IEnumerable<PurchaseOrderBuyerReportViewModel> purchaseOrderBuyerReportViewModels { get; set; }
        public IEnumerable<ApproverPanelViewModel> approerPanelList { get; set; }
        public IEnumerable<RequisitionGRCumulativeViewModel> requisitionGRCumulativeViewModels { get; set; }

        public IEnumerable<POSupplierReportViewModel> pOSupplierReportViewModels { get; set; }

    }
}
