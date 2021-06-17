using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class LocationActivityViewModel
    {
        public int activityId { get; set; }

        public string activityName { get; set; }

        public IEnumerable<WorkProgressActivityType> workProgressActivityTypes { get; set; }
    }
}
