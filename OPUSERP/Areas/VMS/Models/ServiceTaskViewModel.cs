using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class ServiceTaskViewModel
    {
        public int? serviceTaskNameId { get; set; }
        public string serviceTaskName { get; set; }
        public string description { get; set; }
        public string subTaskName { get; set; }
        public IEnumerable<ServiceTask> serviceTasks { get; set; }
    }
}
