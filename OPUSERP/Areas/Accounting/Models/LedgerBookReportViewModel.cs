namespace OPUSERP.Areas.Accounting.Models
{
    public class LedgerBookReportViewModel
    {
        public int? VoucherId { get; set; }
        public string voucherDate { get; set; }
        public string voucherNo { get; set; }
        public string accountName { get; set; }
        public string accountCode { get; set; }
        public string subledgerName { get; set; }
        public string voucherType { get; set; }
        public string action { get; set; }
        //public string url { get; set; }
     
        public decimal? debit { get; set; }
        public decimal? credit { get; set; }
    }
}
