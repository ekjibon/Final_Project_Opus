using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class ActivityNameViewModel
    {
        public int? ActivityNameId { get; set; }

        public int? ActivityType { get; set; }

        [Required]
        public string ActivityName { get; set; }


        public IEnumerable<ActivityName> activityNames { get; set; }
        public IEnumerable<HRPMSActivityType> hRPMSActivityTypes { get; set; }
    }
}
