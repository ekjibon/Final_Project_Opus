using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.FuelInfo
{
    [Table("FuelInformation", Schema = "VMS")]
    public class FuelInformation:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }
        
        public int? fuelStationInfoId { get; set; }
        public FuelStationInfo fuelStationInfo { get; set; }

        public string odometer { get; set; }
        public DateTime? fuelTakenDate { get; set; }
        [MaxLength(350)]
        public string referenceNo { get; set; }        
    }
}
