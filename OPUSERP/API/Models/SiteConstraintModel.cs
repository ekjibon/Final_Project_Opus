using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.API.Models
{
    public class SiteConstraintModel
    {
        public int? progressReportId { get; set; }
        public DailyProgressReport progressReport { get; set; }
        public string name { get; set; }
        public int? statusId { get; set; }
    }
}
