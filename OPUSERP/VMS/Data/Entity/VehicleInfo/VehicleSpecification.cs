using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VehicleSpecification", Schema = "VMS")]
    public class VehicleSpecification:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; } 

        public decimal? width { get; set; }
        public decimal? height { get; set; }
        public decimal? length { get; set; }
        public decimal? interiorVolume { get; set; }
        public decimal? passengerVolume { get; set; }
        public decimal? cargoVolume { get; set; }
        public decimal? groundClearance { get; set; }
        public decimal? bedLength { get; set; }
        public decimal? curbWeight { get; set; }
        public decimal? grossVehicleWeightRating { get; set; }
        public decimal? towingCapacity { get; set; }
        public decimal? maxPayload { get; set; }
        public decimal? epaCity { get; set; }
        public decimal? epaHighway { get; set; }        
        public decimal? epaCombined { get; set; }

    }
}
