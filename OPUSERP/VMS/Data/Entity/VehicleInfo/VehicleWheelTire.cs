using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VehicleWheelTire", Schema = "VMS")]
    public class VehicleWheelTire:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        public int? driveTypeId { get; set; }
        public DriveType driveType { get; set; }

        public int? brakeSystemId { get; set; }
        public BrakeSystem brakeSystem { get; set; }

        public decimal? frontTrackWidth { get; set; }
        public decimal? rearTrackWidth { get; set; }
        public decimal? wheelBase { get; set; }
        [MaxLength(250)]
        public string frontWheelDiameter { get; set; }
        [MaxLength(250)]
        public string rearWheelDiameter { get; set; }
        [MaxLength(250)]
        public string rearAxle { get; set; }
        [MaxLength(250)]
        public string frontTireType { get; set; }       
        public decimal? frontTirePsi { get; set; }
        [MaxLength(250)]
        public string rearTireType { get; set; }
        public decimal? rearTirePsi { get; set; }

    }
}
