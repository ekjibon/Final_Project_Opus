using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VehicleInformation", Schema = "VMS")]
    public class VehicleInformation:Base
    {
        public int? vehicleTypeId { get; set; }
        public VehicleType vehicleType { get; set; }

        public int? vehicleManufactureId { get; set; }
        public VehicleManufacture vehicleManufacture { get; set; }

        public int? vehicleModelId { get; set; }
        public VehicleModel vehicleModel { get; set; }

        public int? vehicleStatusId { get; set; }
        public VehicleStatus vehicleStatus { get; set; }

        public int? vehicleGroupId { get; set; }
        public VehicleGroup vehicleGroup { get; set; }

        public int? vehicleBodyTypeId { get; set; }
        public VehicleBodyType vehicleBodyType { get; set; }

        public int? vehicleBodySubTypeId { get; set; }
        public VehicleBodySubType vehicleBodySubType { get; set; }

        public int? vehicleOwnershipId { get; set; }
        public VehicleOwnership vehicleOwnership { get; set; }

        public int? vehicleLinkedLId { get; set; }

        public string operatorId { get; set; }

        [MaxLength(250)]
        public string vehicleName { get; set; }
        [MaxLength(50)]
        public string vinNo { get; set; }
        [MaxLength(50)]
        public string licensePlate { get; set; }
        [MaxLength(4)]
        public string vehicleYear { get; set; }
        [MaxLength(150)]
        public string trim { get; set; }
        [MaxLength(350)]
        public string registrationStateArea { get; set; }
        [MaxLength(150)]
        public string color { get; set; }
        public decimal? msrPrice { get; set; }

    }
}
