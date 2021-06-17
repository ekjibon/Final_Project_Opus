namespace OPUSERP.Areas.Inventory.Models
{
    public class StockSummaryViewModel
    {
        public string itemName { get; set; }
        public string specificationName { get; set; }

        public int? openQty { get; set; }
        public int? inQty { get; set; }
        public int? outQty { get; set; }
        public int? balance { get; set; }

        public decimal? openAmount { get; set; }
        public decimal? receiveAmount { get; set; }        
        public decimal? issueAmount { get; set; }
        public decimal? balanceAmount { get; set; }
    }
}
