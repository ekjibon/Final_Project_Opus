using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("VehicleServiceIssue", Schema = "VMS")]
    public class VehicleServiceIssue: Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        public DateTime? reportedDate { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public decimal? odometerValue { get; set; }
        public string reportedBy { get; set; }
        public string assignedTo { get; set; }
        public DateTime? dueDate { get; set; }
        public decimal? overdueOdometerValue { get; set; }
    }
}
