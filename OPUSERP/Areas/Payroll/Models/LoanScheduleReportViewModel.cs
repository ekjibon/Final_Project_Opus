namespace OPUSERP.Areas.Payroll.Models
{
    public class LoanScheduleReportViewModel
    {
        public string scheduleDate { get; set; }
        public string paidDate { get; set; }
        public decimal? scheduleAmount { get; set; }
        public decimal? monthlyInstallment { get; set; }
        public decimal? monthlyInterest { get; set; }
        public decimal? openingBalance { get; set; }
        public decimal? closingBalance { get; set; }
        public string paidStatus { get; set; }
    }
}
