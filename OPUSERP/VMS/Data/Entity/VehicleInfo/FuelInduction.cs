using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("FuelInduction", Schema = "VMS")]
    public class FuelInduction:Base
    {
        [MaxLength(250)]
        public string fuelInductionName { get; set; }
        public int? sortOrder { get; set; }
    }
}
