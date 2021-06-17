using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.Master;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("VehicleRenewalReminder", Schema = "VMS")]
    public class VehicleRenewalReminder : Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation  vehicleInformation { get; set; }
        public int? renewalTypeId { get; set; }
        public RenewalType  renewalType { get; set; }
        

        public DateTime? issueDate { get; set; }
        public DateTime? expireDate { get; set; }
        [MaxLength(150)]
        public string reminderType { get; set; }
        [MaxLength(100)]
        public string reminderValue { get; set; }

        [MaxLength(100)]
        public string emailNotifications { get; set; }
        [MaxLength(100)]
        public string emailId { get; set; }
    }
}
