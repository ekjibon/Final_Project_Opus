using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.FuelInfo;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleServiceCommentsViewModel
    {
        public int? commentsId { get; set; }

        public int vehicleServiceHistoryId { get; set; }

        public string titles { get; set; }

        public string comments { get; set; }

        public virtual IEnumerable<VehicleServiceComment>  VehicleServiceComments { get; set; }
    }
}
