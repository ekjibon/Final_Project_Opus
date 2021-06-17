using OPUSERP.Areas.HRPMSEmployee.Models.Dashboard;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class ActivityStatusViewModel
    {
        [Required]
        public string activityId { get; set; }
        public string statusName { get; set; }
        public string statusNameBn { get; set; }
                
        public string shortName { get; set; }

        public ActivityStatusLn fLang { get; set; }

        public IEnumerable<ActivityStatus> activityStatus { get; set; }

        public ActiveEmployeeCountViewModel activeEmployeeCountViewModel { get; set; }
    }
}
