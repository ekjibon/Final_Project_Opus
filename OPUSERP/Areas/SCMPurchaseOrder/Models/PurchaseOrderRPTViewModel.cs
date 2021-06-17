namespace OPUSERP.Areas.SCMPurchaseOrder.Models
{
    public class PurchaseOrderRPTViewModel
    {
        public int? Id { get; set; }
        public string poNo { get; set; }
        //public string csNo { get; set; }
        public string reqNo { get; set; }
        public string billToLocation { get; set; }
        public string empName { get; set; }
        public string OfficMobile { get; set; }
        public string poDate { get; set; }
        public string deliveryDate { get; set; }
        public decimal? poValue { get; set; }
        public string remarks { get; set; }
        public string deliveryModeName { get; set; }
        public string supplierName { get; set; }
        public string supplierAddress { get; set; }
        public string contactPerson { get; set; }
        public string contactPersonMobile { get; set; }
        public string paymentTermsName { get; set; }
        public string projectName { get; set; }
        public string inChargeEmpCode { get; set; }
        public string projectLocation { get; set; }
        public string projectShortName { get; set; }
        public string paymentTermsCode { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public string unitName { get; set; }
        public decimal? poQty { get; set; }
        public string specificationName { get; set; }
        public decimal? poRate { get; set; }
        public decimal? vat { get; set; }
        public decimal? tax { get; set; }
        public decimal? poValueItem { get; set; }
        public decimal? poSubTotal { get; set; }
        public decimal? discount { get; set; }
        public string PIMobile { get; set; }
        public string IsCopy { get; set; }
    }
}
