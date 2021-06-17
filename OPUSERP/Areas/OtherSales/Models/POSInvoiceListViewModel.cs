namespace OPUSERP.Areas.OtherSales.Models
{
    public class POSInvoiceListViewModel
    {
        public int Id { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetTotal { get; set; }
        public decimal? VATOnTotal { get; set; }
        public decimal? DiscountOnTotal { get; set; }
        public string resourceName { get; set; }
        public string mobile { get; set; }

    }
}
