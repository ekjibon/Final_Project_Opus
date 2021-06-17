using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VehicleSetting", Schema = "VMS")]
    public class VehicleSetting:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        public int? meterTypeId { get; set; }
        public MeterType meterType { get; set; }

        public int? unitId { get; set; }
        public Unit unit { get; set; }
        
        public int? sortOrder { get; set; }
    }
}
