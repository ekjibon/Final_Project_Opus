using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class HRProjectViewModel
    {
        public int? Id { get; set; }

        public string name { get; set; }

        public string code { get; set; }

        public IEnumerable<HRProject> hRProjects { get; set; }
    }
}
