using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleRenewalReminderViewModel
    {
        public int vehicleRenewalReminderId { get; set; }
        public int? vehicleInformationId { get; set; }
        public int? renewalTypeId { get; set; }
        public DateTime? issueDate { get; set; }
        public DateTime? expireDate { get; set; }
        public string reminderType { get; set; }
        public string reminderValue { get; set; }
        public string emailNotifications { get; set; }
        public string emailId { get; set; }



        public IEnumerable<VehicleRenewalReminder>  vehicleRenewalReminders { get; set; }
        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }
        public IEnumerable<RenewalType>   renewalTypes { get; set; }
    }
}
