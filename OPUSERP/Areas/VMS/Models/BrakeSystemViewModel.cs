using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class BrakeSystemViewModel
    {
        public int? brakeSystemId { get; set; }
        public string brakeSystemName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<BrakeSystem> brakeSystems { get; set; }
    }
}
