using System.Collections.Generic;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleCommentsViewModel
    {
        public int? commentsId { get; set; }

        public int vehicleInformationId { get; set; }

        public string titles { get; set; }

        public string comments { get; set; }

        public virtual IEnumerable<VehicleComment> vehicleComments { get; set; }
    }
}
