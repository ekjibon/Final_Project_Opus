using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("FuelType", Schema = "VMS")]
    public class FuelType:Base
    {
        [MaxLength(250)]
        public string fuelTypeName { get; set; }
        public int? sortOrder { get; set; }
        public string unit { get; set; }
    }
}
