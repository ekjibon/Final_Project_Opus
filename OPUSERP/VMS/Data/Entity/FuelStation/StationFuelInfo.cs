using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.FuelStation
{
    [Table("StationFuelInfo", Schema = "VMS")]
    public class StationFuelInfo:Base
    {
        public int? fuelStationInfoId { get; set; }
        public FuelStationInfo fuelStationInfo { get; set; }

        public int? fuelTypeId { get; set; }
        public FuelType fuelType { get; set; }

        public int? noOfNozlzle { get; set; }
    }
}
