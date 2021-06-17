using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("Odometer", Schema = "VMS")]
    public class Odometer:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        [MaxLength(250)]
        public string odometerValue { get; set; }
        public int? sortOrder { get; set; }

        [MaxLength(250)]
        public string sourceType { get; set; }
        public int? sourceTypeId { get; set; }
        public DateTime? readingDate { get; set; }
    }
}
