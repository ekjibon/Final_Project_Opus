using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("VehicleServiceReminder", Schema = "VMS")]
    public class VehicleServiceReminder:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        //public int? serviceTaskId { get; set; }
        //public ServiceTask serviceTask { get; set; }
        public int? sparePartsId { get; set; }
        public SpareParts spareParts { get; set; }

        public decimal? meterInterval { get; set; }
        public int? timeInterval { get; set; }
        [MaxLength(10)]
        public string timeIntervalType { get; set; }

        [MaxLength(3)]
        public string setManualReminder { get; set; }
        public decimal? manualOdometer { get; set; }
        [MaxLength(10)]
        public string manualMonth { get; set; }
        public int? manualDay { get; set; }
        public int? manualYear { get; set; }

        public decimal? dueMeterThreshold { get; set; }
        public int? dueTimeThreshold { get; set; }
        [MaxLength(10)]
        public string dueTimeThresholdType { get; set; }

        [MaxLength(3)]
        public string sendEmailNotification { get; set; }
        public string comments { get; set; }
        
    }
}
