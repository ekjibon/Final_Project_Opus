using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleGroupViewModel
    {
        public int? vehicleGroupId { get; set; }
        public string vehicleGroupName { get; set; }

        public int? sortOrder { get; set; }

        public IEnumerable<VehicleGroup> vehicleGroups { get; set; }
    }
}
