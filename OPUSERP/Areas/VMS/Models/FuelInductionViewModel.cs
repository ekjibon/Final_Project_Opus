using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class FuelInductionViewModel
    {
        public int? fuelInductionId { get; set; }
        public string fuelInductionName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<FuelInduction> fuelInductions { get; set; }
    }
}
