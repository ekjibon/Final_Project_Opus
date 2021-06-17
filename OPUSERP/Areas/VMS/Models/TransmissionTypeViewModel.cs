using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class TransmissionTypeViewModel
    {
        public int? transmissionTypeId { get; set; }
        public string transmissionTypeName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<TransmissionType> transmissionTypes { get; set; }
    }
}
