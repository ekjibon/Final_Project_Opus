namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportSixPointSixViewModel
    {
       
        public string organizationName { get; set; }
        public string companyName { get; set; }
        public string tinNo { get; set; }
        public string tradeLicense { get; set; }
        public string binNo { get; set; }
        public string officerName { get; set; }
        public string designation { get; set; }
        public string supplierBinNo { get; set; }
        public string poNo { get; set; }
        public string poDate { get; set; }
        public string address { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? vatDeductionAmount { get; set; }
        public decimal? vatAmount { get; set; }

    }
}
