namespace OPUSERP.Areas.SCMPurchaseOrder.Models
{
    public class CSSupplierVM
    {
        public int? supplierId { get; set; }
        public string orgCode { get; set; }
        public string organizationName { get; set; }
        public decimal? total { get; set; }
        public string personName { get; set; }
        public string contactNo { get; set; }
    }
}
