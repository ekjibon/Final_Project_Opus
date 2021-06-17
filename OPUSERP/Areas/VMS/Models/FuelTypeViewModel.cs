using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class FuelTypeViewModel
    {
        public int? fuelTypeId { get; set; }
        public string fuelTypeName { get; set; }
        public int? sortOrder { get; set; }
        public string unit { get; set; }
        public IEnumerable<FuelType> fuelTypes { get; set; }
    }
}
