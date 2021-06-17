
namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportSixPointFourViewModel
    {
        public string organizationName { get; set; }
        public string companyName { get; set; }
        public string tinNo { get; set; }
        public string tradeLicense { get; set; }
        public string binNo { get; set; }
        public string invoiceNumber { get; set; }
        public string invoiceDate { get; set; }
        public string supplierBinNo { get; set; }
        public string debitNoteNo { get; set; }
        public string issueDate { get; set; }
        public string issuetime { get; set; }
        public string itemName { get; set; }
        public string unitName { get; set; }

        public decimal? quantity { get; set; }
        public decimal? rate { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? vatOnTotal { get; set; }
        public decimal? netAmount { get; set; }
        public string officerName { get; set; }
        public string designation { get; set; }

    }
}
