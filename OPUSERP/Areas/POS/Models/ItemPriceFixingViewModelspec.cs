namespace OPUSERP.Areas.POS.Models
{
    public class ItemPriceFixingViewModelspec
    {
        public int? Id { get; set; }
        public int? itemSpecificationId { get; set; }
        public string itemSpecificationName { get; set; }
        public decimal? price { get; set; }
        public decimal? discountPersent { get; set; }
        public decimal? VAT { get; set; }
        public string unitName { get; set; }
        public string barcode { get; set; }
        public int? isActive { get; set; }  // 0 = In Active, 1 = Active

        //Newly Added 10/02/21
        public string specImageUrl { get; set; }
        public string description { get; set; }
    }
}
