using System.Collections.Generic;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Requisition;

namespace OPUSERP.Areas.VMS.Models
{
    public class RequisitionCommentsViewModel
    {
        public int? commentsId { get; set; }

        public int? requisitionMasterId { get; set; }

        public string titles { get; set; }

        public string comments { get; set; }

        public virtual IEnumerable<RequisitionComment> RequisitionComments { get; set; }
    }
}
