using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class AspirationViewModel
    {
        public int? aspirationId { get; set; }
        public string aspirationName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<Aspiration> aspirations { get; set; }
    }
}
