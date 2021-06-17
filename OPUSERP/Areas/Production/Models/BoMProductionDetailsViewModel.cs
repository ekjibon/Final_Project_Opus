namespace OPUSERP.Areas.Production.Models
{
    public class BoMProductionDetailsViewModel
    {
        public int? Id { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public string unitName { get; set; }
        public decimal? quantity { get; set; }
        public decimal? productQty { get; set; }
        public decimal? productPrice { get; set; }
        public decimal? wastePercent { get; set; }
        public int? supplierId { get; set; }
    }
}
