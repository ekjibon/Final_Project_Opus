using System;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.Inspection
{
    [Table("InspectionMaster", Schema = "VMS")]
    public class InspectionMaster:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; } 

        public decimal? odometer { get; set; }

        public string signature { get; set; }

        public DateTime? inspectionDate { get; set; }
    }
}
