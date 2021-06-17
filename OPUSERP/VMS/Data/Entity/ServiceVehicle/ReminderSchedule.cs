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
    [Table("ReminderSchedule", Schema = "VMS")]
    public class ReminderSchedule:Base
    {
        public int? vehicleServiceReminderId { get; set; }
        public VehicleServiceReminder vehicleServiceReminder { get; set; }

        public decimal? scheduleOdometer { get; set; }
        public DateTime? scheduleDate { get; set; }
       
        
    }
}
