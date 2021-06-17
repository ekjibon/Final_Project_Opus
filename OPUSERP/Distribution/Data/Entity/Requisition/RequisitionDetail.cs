using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Data.Entity.Requisition
{
    [Table("RequisitionDetail", Schema = "Distribution")]
    public class RequisitionDetail : Base
    {
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public int? requisitionMasterId { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }
        public decimal? quantity { get; set; }

        public int? disItemPriceFixingId { get; set; }
        public DisItemPriceFixing disItemPriceFixing { get; set; }
        public decimal? rate { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? sdAmount { get; set; }
        public decimal? cdAmount { get; set; }
        public decimal? atAmount { get; set; }
        public decimal? rdAmount { get; set; }
        public decimal? lineTotal { get; set; }
        public decimal? discountAmount { get; set; }

        public decimal? appvatAmount { get; set; }
        public decimal? apptaxAmount { get; set; }
        public decimal? appsdAmount { get; set; }
        public decimal? appcdAmount { get; set; }
        public decimal? appatAmount { get; set; }
        public decimal? apprdAmount { get; set; }
        public decimal? applineTotal { get; set; }
        public decimal? appdiscountAmount { get; set; }

        public int? packageDetailId { get; set; }
        public PackageDetail packageDetail { get; set; }


    }
}
