using System.Collections.Generic;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class BasicReportViewModel
    {
        public int reportTypeId { get; set; }

        public int vehicleId { get; set; }

        public int fuelStationId { get; set; }

        public int vendorId { get; set; }

        public string fromDate { get; set; }

        public string toDate { get; set; }

        public IEnumerable<FuelStationInfo> fuelStationInfos { get; set; }

        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }

        public IEnumerable<VMSSupplier> suppliers { get; set; }
    }
}
