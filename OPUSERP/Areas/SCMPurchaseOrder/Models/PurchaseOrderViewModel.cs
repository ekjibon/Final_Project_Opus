using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Stock;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMPurchaseOrder.Models
{
    public class PurchaseOrderViewModel
    {
        public int PurchaseOrderId { get; set; }
        public string poNo { get; set; }
        public DateTime? poDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public int? deliveryType { get; set; }
        public int? paymentType { get; set; }
        public int? paymenTerm { get; set; }
        public string remarks { get; set; }
        public int? txtSupplierId { get; set; }
        public int? txtRequsitionId { get; set; }
        public int? txtCsmasterId { get; set; }
        public string billToLocation { get; set; }

        public decimal? savingsAmount { get; set; }
        public decimal? savingsPercent { get; set; }
        public string content { get; set; }

        public int?[] csDetailsall { get; set; }
        public int?[] currencyall { get; set; }
        public int?[] txtLocationall { get; set; }
        public decimal?[] txtVatall { get; set; }
        public decimal?[] poQntall { get; set; }
        public decimal?[] txtUnitRateall { get; set; }
        public decimal?[] txtAmtExVatall { get; set; }
        public decimal?[] txtVatAmountall { get; set; }
        public decimal?[] txtAitall { get; set; }
        public decimal?[] txtAitAmountall { get; set; }
        public string[] txtDescriptionall { get; set; }
        public string[] txtOtherLocationall { get; set; }

        public IEnumerable<CSMaster> cSMasters { get; set; }
        public IEnumerable<DeliveryMode> deliveryModes { get; set; }
        public IEnumerable<DeliveryLocation> deliveryLocations { get; set; }
        public IEnumerable<PaymentMode> paymentModes { get; set; }
        public IEnumerable<PaymentTerms> paymentTerms { get; set; }
        public IEnumerable<Currency> currencies { get; set; }
        public IEnumerable<PurchaseOrderMaster> purchaseOrderMasters { get; set; }
        public IEnumerable<PurchaseOrderMaster> issuedpurchaseOrder { get; set; }
        public IEnumerable<IOUMaster> iOUMasters { get; set; }
        public IEnumerable<StockMaster> stockMasters { get; set; }
        
    }
}
