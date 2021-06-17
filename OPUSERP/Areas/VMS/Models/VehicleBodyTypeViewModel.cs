using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleBodyTypeViewModel
    {
        public int? vehicleBodyTypeId { get; set; }
        public string vehicleBodyTypeName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<VehicleBodyType> vehicleBodyTypes { get; set; }
    }
}
