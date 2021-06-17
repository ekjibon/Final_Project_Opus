using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("BrakeSystem", Schema = "VMS")]
    public class BrakeSystem:Base
    {
        [MaxLength(250)]
        public string brakeSystemName { get; set; }
        public int? sortOrder { get; set; }
    }
}
