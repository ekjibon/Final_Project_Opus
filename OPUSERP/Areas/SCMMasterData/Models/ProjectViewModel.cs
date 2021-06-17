using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ProjectViewModel
    {
        public int? projectId { get; set; }

        public int? sbuId { get; set; }

        public string projectName { get; set; }

        public string projectNameBN { get; set; }

        public string projectShortName { get; set; }

        public string projectLocation { get; set; }

        public string inChargeEmpCode { get; set; }
        public int? isdefault { get; set; }
        public string projectStatus { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public IEnumerable<Project> projects { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
    }
}
