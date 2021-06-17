namespace OPUSERP.Areas.Inventory.Models
{
    public class StockBalanceByItemViewModel
    {
        public string stockDate { get; set; }
        public string itemName { get; set; }
        public string specificationName { get; set; }
        public int? itemSpecificationId { get; set; }
        public decimal? openQty { get; set; }
        public decimal? openRate { get; set; }
        public decimal? openValue { get; set; }
        public decimal? inQty { get; set; }
        public decimal? purchaseRate { get; set; }
        public decimal? inValue { get; set; }
        public decimal? totalQty { get; set; }
        public decimal? totalValue { get; set; }
        public decimal? outQty { get; set; }
        public decimal? outValue { get; set; }
        public decimal? totalOutQty { get; set; }
        public decimal? totalOutValue { get; set; }
        public decimal? closingQty { get; set; }
        public decimal? closingValue { get; set; }
    }
}
