using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class LimitAmountTypeViewModel
    {
        public int? limitAmountTypeId { get; set; }
        public string limitAmountTypeName { get; set; }
        
        public IEnumerable<LimitAmountType> limitAmountTypes { get; set; }
    }
}
