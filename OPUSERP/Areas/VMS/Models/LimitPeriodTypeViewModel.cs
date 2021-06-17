using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class LimitPeriodTypeViewModel
    {
        public int? limitPeriodTypeId { get; set; }
        public string limitPeriodTypeName { get; set; }
        
        public IEnumerable<LimitPeriodType> limitPeriodTypes { get; set; }
    }
}
