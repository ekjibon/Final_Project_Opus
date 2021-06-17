using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.Supplier;
//using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("RFQReqDetailPrice", Schema = "SCM")]
    public class RFQReqDetailPrice : Base
    {
        public int? rFQPriceMasterId { get; set; }
        public RFQPriceMaster rFQPriceMaster { get; set; }
        public int? rFQReqDetailId { get; set; }
        public RFQReqDetail rFQReqDetail { get; set; }
        public decimal? rate { get; set; }
       
    }
}
