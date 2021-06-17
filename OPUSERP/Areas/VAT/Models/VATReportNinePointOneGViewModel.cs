
namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportNinePointOneGViewModel
    {
        public string hsCode { get; set; }
        public string itemName { get; set; }
        public string unitName { get; set; }
        public decimal? quantity { get; set; }
        public decimal? price { get; set; }
        public decimal? sd { get; set; }
        public decimal? vat { get; set; }
    }
}
