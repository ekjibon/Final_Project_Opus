using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleServiceReminderViewModel
    {
        public int vehicleServiceReminderId { get; set; }
        public int? vehicleInformationId { get; set; }
        public int? serviceTaskId { get; set; }
        public int? reminderScheduleId { get; set; }
        public int? vehicleInformationIdReminder { get; set; }
        public int? serviceTaskIdReminder { get; set; }
        

        public decimal? meterInterval { get; set; }
        public int? timeInterval { get; set; }        
        public string timeIntervalType { get; set; }
        public string setManualReminder { get; set; }
        public decimal? manualOdometer { get; set; }
        public string manualMonth { get; set; }
        public int? manualDay { get; set; }
        public int? manualYear { get; set; }
        public decimal? dueMeterThreshold { get; set; }
        public int? dueTimeThreshold { get; set; }
        public string dueTimeThresholdType { get; set; }
        public string sendEmailNotification { get; set; }
        public string comments { get; set; }

        public decimal?[] scheduleOdometer { get; set; }
        public DateTime?[] scheduleDate { get; set; }

       

        public IEnumerable<VehicleServiceReminder> vehicleServiceReminders { get; set; }
        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }
        public IEnumerable<VehicleComment> vehicleComments { get; set; }
        public IEnumerable<ServiceTask> serviceTasks { get; set; }
        public IEnumerable<VehicleStatus> vehicleStatuses { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<ReminderSchedule> reminderSchedules { get; set; }
        public IEnumerable<SpareParts> spareParts { get; set; }

        public VehicleServiceReminder vehicleServiceReminder { get; set; }
        public VehicleInformation VehicleInformationbyid { get; set; }
        public Odometer odometer { get; set; }
    }
}
