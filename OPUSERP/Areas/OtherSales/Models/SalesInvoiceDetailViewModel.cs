using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models
{
    public class SalesInvoiceDetailViewModel
    {
        public int Id { get; set; }
        public int itemId { get; set; }
        public int itemSpecicationId { get; set; }
        public string itemName { get; set; }
        public string specificationName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? lineTotal { get; set; }
        public decimal? quantity { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? disAmount { get; set; }
    }
}
