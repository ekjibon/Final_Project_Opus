using System;

namespace OPUSERP.Areas.SCMReport.Models
{
    public class RequisitionGRCumulativeViewModel
    {
        public string itemName { get; set; }
        public string specificationName { get; set; }
        public string unitName { get; set; }
        public DateTime? targetDate { get; set; }
        public string justification { get; set; }
        public decimal? reqQty { get; set; }
        public decimal? reqRate { get; set; }
        public decimal? cumGrQty { get; set; }

        //SP_GetRequisitionDetailsByReqId,SP_GetItemWiseCumutativeQTY
    }
}
