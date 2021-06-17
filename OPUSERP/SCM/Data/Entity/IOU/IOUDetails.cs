using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.IOU
{
    [Table("IOUDetails", Schema = "SCM")]
    public class IOUDetails:Base
    {
        public int? IOUId { get; set; }
        public IOUMaster IOU { get; set; }

        public int? requisitionDetailId { get; set; }
        public RequisitionDetail requisitionDetail { get; set; }

        public decimal? rate { get; set; }

        public decimal? qty { get; set; }

        public decimal? vatPercent { get; set; }

        public decimal? vatAmount { get; set; }

        public int? currencyId { get; set; }
        public Currency currency { get; set; }

        public decimal? currencyRate { get; set; }

        public int? deliveryLocationId { get; set; }
        public DeliveryLocation deliveryLocation { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string otherDeliveryLocation { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string description { get; set; }

        public int? iou_DStatus { get; set; }

        public int? requisitionId { get; set; }
        //public RequisitionMaster requisition { get; set; }
    }
}
