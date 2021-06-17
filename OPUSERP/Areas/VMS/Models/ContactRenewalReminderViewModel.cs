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
    public class ContactRenewalReminderViewModel
    {
        public int contactRenewalReminderId { get; set; }
        public int? supplierId { get; set; }
        public int? renewalTypeId { get; set; }
        public DateTime? issueDate { get; set; }
        public DateTime? expireDate { get; set; }
        public string reminderType { get; set; }
        public string reminderValue { get; set; }
        public string emailNotifications { get; set; }
        public string emailId { get; set; }



        public IEnumerable<ContactRenewalReminder>  contactRenewalReminders { get; set; }
        public IEnumerable<VMSSupplier>  suppliers { get; set; }
        public IEnumerable<RenewalType>   renewalTypes { get; set; }
    }
}
