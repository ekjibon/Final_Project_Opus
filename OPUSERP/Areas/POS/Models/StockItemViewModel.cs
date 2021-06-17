using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Areas.POS.Models
{
    [NotMapped]
    public class StockItemViewModel
    {
        public int? Id { get; set; }
        public int? purchaseId { get; set; }
        public int? itemSpecificationId { get; set; }
        public string description { get; set; }
        public int? deliveryLoacationId { get; set; }
        public decimal? quantity { get; set; }
        public decimal? dueQuantity { get; set; }
        public decimal? rate { get; set; }
        public int? currencyId { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? taxPercent { get; set; }
        public string itemName { get; set; }
        public string itemSpecificationName { get; set; }
        public string PONO { get; set; }
        public string unitName { get; set; }
        public int? ItemPriceId { get; set; }
    }
}
