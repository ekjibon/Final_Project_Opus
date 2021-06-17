using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("VehicleMaintenanceLimitDetail", Schema = "VMS")]
    public class VehicleMaintenanceLimitDetail:Base
    {
        public int? vehicleMaintenanceLimitId { get; set; }
        public VehicleMaintenanceLimit vehicleMaintenanceLimit { get; set; }
        public int? fuelTypeId { get; set; }
        public FuelType fuelType { get; set; }
        public int? limitPeriodTypeId { get; set; }
        public LimitPeriodType limitPeriodType { get; set; } 
        
        public int? limitAmountTypeId { get; set; }
        public LimitAmountType limitAmountType { get; set; }
     
        public decimal? limitValue { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }
}
