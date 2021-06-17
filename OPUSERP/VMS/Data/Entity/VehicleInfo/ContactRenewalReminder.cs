using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("ContactRenewalReminder", Schema = "VMS")]
    public class ContactRenewalReminder : Base
    {
        public int? supplierId { get; set; }
        public Supplier supplier { get; set; }

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
