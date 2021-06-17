using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleModelViewModel
    {
        public int? vehicleModelId { get; set; }
        public string vehicleModelName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<VehicleModel> vehicleModels { get; set; }
    }
}
