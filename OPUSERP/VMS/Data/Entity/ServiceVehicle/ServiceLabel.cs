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
    [Table("ServiceLabel", Schema = "VMS")]
    public class ServiceLabel: Base
    {
        public int? vehicleServiceHistoryId { get; set; }
        public VehicleServiceHistory vehicleServiceHistory { get; set; }

        public int? vehicleInformationId { get; set; }   
        public int? labelTypeId { get; set; }
        public LabelType labelType { get; set; }        
        
    }
}
