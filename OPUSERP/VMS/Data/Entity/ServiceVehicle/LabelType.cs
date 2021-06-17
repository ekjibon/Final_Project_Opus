using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("LabelType", Schema = "VMS")]
    public class LabelType: Base
    {

        [MaxLength(250)]
        public string labelTypeName { get; set; }
        
        public int? sortOrder { get; set; }
    }
}
