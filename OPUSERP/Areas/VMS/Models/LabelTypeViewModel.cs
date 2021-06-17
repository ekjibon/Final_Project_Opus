using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class LabelTypeViewModel
    {
        public int? labelTypeId { get; set; }
        public string labelTypeName { get; set; }
        public int? sortOrder { get; set; }
        public IEnumerable<LabelType> labelTypes{ get; set; }
    }
}
