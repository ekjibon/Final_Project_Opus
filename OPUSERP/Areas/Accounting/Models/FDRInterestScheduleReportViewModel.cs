namespace OPUSERP.Areas.Accounting.Models
{
    public class FDRInterestScheduleReportViewModel
    {
        public string AccountName { get; set; }
        public string Tenure { get; set; }
        public string Formula { get; set; }
        public string DebitBank { get; set; }
        public string InvestBank { get; set; }
        public string OpenDate { get; set; }
        public string MaturityDate { get; set; }
        public string YearName { get; set; }

        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalInterest { get; set; }

        public decimal? January { get; set; }
        public decimal? February { get; set; }
        public decimal? March { get; set; }
        public decimal? April { get; set; }
        public decimal? May { get; set; }
        public decimal? June { get; set; }
        public decimal? July { get; set; }
        public decimal? August { get; set; }        
        public decimal? September { get; set; }
        public decimal? October { get; set; }
        public decimal? November { get; set; }
        public decimal? December { get; set; }
        
    }
}
