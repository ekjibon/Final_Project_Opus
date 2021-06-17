using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.VMS.Data.Entity.CarManagement;

namespace OPUSERP.Areas.VMS.Models
{
    [NotMapped]
    public class VehicleInformationViewModelOld
    {
        public int? ID { get; set; }

        public string typeOfVehicle { get; set; }

        public string modelno { get; set; }

        public string brand { get; set; }

        public string regNo { get; set; }

        public string mileage { get; set; }

        public string capacity { get; set; }

        public string color { get; set; }

        public string engineCapacity { get; set; }

        public string fuelType { get; set; }

        public int sourceTypeId { get; set; }

        public string chassisNo { get; set; }

        public string licenseNo { get; set; }

        public string eToken { get; set; }

        public string taxToken { get; set; }

        //public VehicleInformationLN flang { get; set; }

        public IEnumerable<OPUSERP.VMS.Data.Entity.VehicleInfo.VehicleInformation> vehicleInformations { get; set; }
        public IEnumerable<SourceType> sourceTypes { get; set; }
    }
}
