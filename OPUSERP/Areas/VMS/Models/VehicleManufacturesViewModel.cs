using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleManufacturesViewModel
    {
        public int? vehicleManufactureId { get; set; }
        public string vehicleManufactureName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<VehicleManufacture> vehicleManufactures { get; set; }
    }
}
