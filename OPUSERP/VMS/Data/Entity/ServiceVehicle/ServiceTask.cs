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
    [Table("ServiceTask", Schema = "VMS")]
    public class ServiceTask: Base
    {
        [MaxLength(350)]
        public string serviceTaskName { get; set; }
        public string description { get; set; }
        [MaxLength(350)]
        public string subTaskName { get; set; }
       
        
    }
}
