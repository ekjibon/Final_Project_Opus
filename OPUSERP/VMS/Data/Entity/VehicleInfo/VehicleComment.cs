using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VehicleComment", Schema = "VMS")]
    public class VehicleComment:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        [MaxLength(250)]
        public string titles { get; set; }
        public string comments { get; set; }
    }
}
