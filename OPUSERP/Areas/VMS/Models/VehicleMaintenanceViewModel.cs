using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Inspection;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleMaintenanceViewModel
    {
        public int? vehicleMaintenanceLimitId { get; set; }
        
        public int? vehicleInformationId { get; set; }
        public int?[] vehicleMaintenanceLimitDetailId { get; set; }
        public int?[] fuelTypeId { get; set; }
        public int?[] limitPeriodTypeId { get; set; }
        public int?[] limitAmountTypeId { get; set; }
        public decimal?[] limitValue { get; set; }
        public DateTime?[] fromDate { get; set; }
        public DateTime?[] toDate { get; set; }

        //public IFormFile[] formFile { get; set; }






        public IEnumerable<VehicleMaintenanceLimit> vehicleMaintenanceLimits { get; set; }
        public VehicleMaintenanceLimit vehicleMaintenanceLimitbyId { get; set; }
        public IEnumerable<VehicleMaintenanceLimitDetail> vehicleMaintenanceLimitDetails { get; set; }
        public IEnumerable<FuelType> fuelTypes { get; set; }
        public IEnumerable<LimitAmountType> limitAmountTypes { get; set; }
        public IEnumerable<LimitPeriodType> limitPeriodTypes { get; set; }
        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }






    }
}
