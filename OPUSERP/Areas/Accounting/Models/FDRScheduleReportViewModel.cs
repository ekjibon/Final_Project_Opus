namespace OPUSERP.Areas.Accounting.Models
{
    public class FDRScheduleReportViewModel
    {
        public string accountName { get; set; }
        public string openDate { get; set; }
        public string maturityDate { get; set; }
        public string refNo { get; set; }

        public decimal? rate { get; set; }
        public decimal? totalInterest { get; set; }
        public decimal? amount { get; set; }
        public decimal? openingAmount { get; set; }
        public decimal? investAmount { get; set; }

        public decimal? toDaysProvision { get; set; }
        public decimal? currentMonthProvision { get; set; }
        public decimal? cumalitiveProvision { get; set; }
        public decimal? openingProvision { get; set; }
        public decimal? currentYearProvision { get; set; }
        public decimal? totalProvision { get; set; }
        
        public decimal? encashAmount { get; set; }
        public decimal? receiveAmount { get; set; }
        public decimal? balanceAmount { get; set; }
        public decimal? interestBalanceAmount { get; set; }
        public decimal? interestAccruedUpto { get; set; }
        public decimal? monthlyInterestBalanceAmount { get; set; }
    }
}
