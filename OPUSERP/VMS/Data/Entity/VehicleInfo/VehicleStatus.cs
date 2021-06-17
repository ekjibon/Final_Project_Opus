using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VehicleStatus", Schema = "VMS")]
    public class VehicleStatus:Base
    {
        [MaxLength(250)]
        public string vehicleStatusName { get; set; }
        [MaxLength(250)]
        public string colorCode { get; set; }
        public int? sortOrder { get; set; }
        [NotMapped]
        public int? count { get; set; }
    }
}
