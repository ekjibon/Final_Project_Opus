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
    [Table("VehicleLineItem", Schema = "VMS")]
    public class VehicleLineItem: Base
    {
        public int? vehicleServiceHistoryId { get; set; }
        public VehicleServiceHistory vehicleServiceHistory { get; set; }

        //public int? serviceTaskId { get; set; }
        //public ServiceTask serviceTask { get; set; }
        public int? sparePartsId { get; set; }
        public SpareParts spareParts { get; set; }

        public decimal? labor { get; set; }
        public decimal? parts { get; set; }
        public decimal? subTotal { get; set; }
        
        
    }
}
