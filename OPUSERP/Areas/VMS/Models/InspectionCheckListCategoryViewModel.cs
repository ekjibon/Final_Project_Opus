using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class InspectionCheckListCategoryViewModel
    {
        public int? inspectionCheckListCategoryId { get; set; }
        public string inspectionCheckListCategoryName { get; set; }
        
        public IEnumerable<InspectionCheckLIstCategory> inspectionCheckLIstCategories { get; set; }
    }
}
