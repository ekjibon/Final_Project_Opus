using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VehicleEngineTransmission", Schema = "VMS")]
    public class VehicleEngineTransmission : Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        public int? aspirationId { get; set; }
        public Aspiration aspiration{ get; set; }

        public int? blockTypeId { get; set; }
        public BlockType blockType { get; set; }

        public int? camTypeId { get; set; }
        public CamType camType { get; set; }

        public int? fuelInductionId { get; set; }
        public FuelInduction fuelInduction { get; set; }

        public int? transmissionTypeId { get; set; }
        public TransmissionType transmissionType{ get; set; }

        public string engineSummary { get; set; }
        [MaxLength(250)]
        public string engineBrand { get; set; }
        public decimal? bore { get; set; }
        public decimal? compression { get; set; }
        public decimal? cylinders { get; set; }
        public decimal? displacement { get; set; }
        [MaxLength(250)]
        public string fuelQuality { get; set; }
        public decimal? maxHp { get; set; }
        public decimal? maxTorque { get; set; }
        [MaxLength(250)]
        public string redLineRpm { get; set; }
        public decimal? stroke { get; set; }
        public decimal? valves { get; set; }
        public string transmissionSummary { get; set; }
        [MaxLength(250)]
        public string transmissionBrand { get; set; }        
        public decimal? transmissionGears { get; set; }

    }
}
