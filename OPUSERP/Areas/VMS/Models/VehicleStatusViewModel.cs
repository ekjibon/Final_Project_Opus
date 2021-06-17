using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleStatusViewModel
    {
        public int? vehicleStatusId { get; set; }
        public string vehicleStatusName { get; set; }
        public string colorCode { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<VehicleStatus> vehicleStatuses { get; set; }
    }
}
