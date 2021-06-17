using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class StoreDepartmentTypeViewModel
    {
        public int id { get; set; }

        public string deptName { get; set; }

        public string deptNameBn { get; set; }
        public int? shortOrder { get; set; }
        public IEnumerable<StoreDepartmentType> StoreDepartmentTypes { get; set; }
    }
}
