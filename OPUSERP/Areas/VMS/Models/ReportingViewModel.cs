using System.Collections.Generic;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class ReportingViewModel
    {
        public string fieldValue { get; set; }

        public string originName { get; set; }

        public IEnumerable<ReportField> reportFields { get; set; }
        
    }
}
