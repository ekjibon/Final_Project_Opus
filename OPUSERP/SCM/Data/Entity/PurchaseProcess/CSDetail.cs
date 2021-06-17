using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Supplier;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseProcess
{
    [Table("CSDetail", Schema = "SCM")]
    public class CSDetail:Base
    {
        public int? cSMasterId { get; set; }
        public CSMaster cSMaster { get; set; }

        public int? requisitionDetailId { get; set; }
        public RequisitionDetail requisitionDetail { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? qty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? rate { get; set; }

        public int? itemCategoryId { get; set; }
        public CSItemCategory itemCategory { get; set; }
        public int? rFQReqDetailPriceId { get; set; }
        public RFQReqDetailPrice rFQReqDetailPrice { get; set; }
        public int? supplierId { get; set; }
        public Organization supplier { get; set; }

        public int? currentStatus { get; set; }
    }
}
