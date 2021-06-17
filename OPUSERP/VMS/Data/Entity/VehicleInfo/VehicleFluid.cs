using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VehicleFluid", Schema = "VMS")]
    public class VehicleFluid:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        public int? fuelTypeId { get; set; }
        public FuelType fuelType { get; set; }

        [MaxLength(250)]
        public string fuelQuality { get; set; }
        public string fuelTankNo { get; set; }
        //public decimal? fuelTank1Capacity { get; set; }
        //public decimal? fuelTank2Capacity { get; set; }
        public decimal? capacity { get; set; }

    }
}
