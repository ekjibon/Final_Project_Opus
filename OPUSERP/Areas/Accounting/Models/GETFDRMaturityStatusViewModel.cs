namespace OPUSERP.Areas.Accounting.Models
{
    public class GETFDRMaturityStatusViewModel
    {
        public string fTInstructionNo { get; set; }
        public string bankName { get; set; }
        public int? fDRID { get; set; }
        public decimal? rate { get; set; }
        public decimal? amount { get; set; }
        public string accountNo { get; set; }
        public string accountName { get; set; }
        public string ftDate { get; set; }
        public string maturityDate { get; set; }
        public string tenure { get; set; }

    }
}
