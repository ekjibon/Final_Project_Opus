using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Rental.Data.Entity.Sales
{
    [Table("RentInvoiceDetail", Schema = "Rental")]
    public class RentInvoiceDetail : Base
    {
        public int? itemPriceFixingId { get; set; }
        public ItemPriceFixing itemPriceFixing { get; set; }

        public int? itemSpecicationId { get; set; }
        public ItemSpecification itemSpecication { get; set; }

        public int? salesInvoiceMasterId { get; set; }
        public RentInvoiceMaster  salesInvoiceMaster { get; set; }
        public decimal? quantity { get; set; }
        public decimal? rate { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? sdAmount { get; set; }
        public decimal? cdAmount { get; set; }
        public decimal? atAmount { get; set; }
        public decimal? rdAmount { get; set; }
        public decimal? disAmount { get; set; }
        public decimal? lineTotal { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [MaxLength(20)]
        public string StartTime { get; set; }
        [MaxLength(20)]
        public string EndTime { get; set; }
    }
}
