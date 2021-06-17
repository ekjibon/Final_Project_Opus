using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleTypeViewModel
    {
        public int? vehicleTypeId { get; set; }
        public string vehicleTypeName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<VehicleType> vehicleTypes { get; set; }
    }
}
