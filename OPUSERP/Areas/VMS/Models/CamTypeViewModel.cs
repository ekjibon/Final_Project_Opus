using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class CamTypeViewModel
    {
        public int? camTypeId { get; set; }
        public string camTypeName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<CamType> camTypes { get; set; }
    }
}
