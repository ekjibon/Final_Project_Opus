using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("VehicleServiceIssueComment", Schema = "VMS")]
    public class VehicleServiceIssueComment : Base
    {
        public int? vehicleServiceIssueId { get; set; }
        public VehicleServiceIssue vehicleServiceIssue{ get; set; }

        [MaxLength(250)]
        public string title { get; set; }        
        public string comment { get; set; }
    }
}
