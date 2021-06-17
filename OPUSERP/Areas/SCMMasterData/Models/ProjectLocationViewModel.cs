using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ProjectLocationViewModel
    {
        public int? projectLocationId { get; set; }
        public int? projectId { get; set; }
        public string locationPosition { get; set; }
        public string floorNo { get; set; }
        public string remarks { get; set; }

        public int[] headlineAll { get; set; }

        public string[] itemSpecificationName { get; set; }
        public int?[] itemSpecificationId { get; set; }
        public string[] CategoryValue { get; set; }

        public string userName { get; set; }
        public int? userId { get; set; }
        public string applicationUserId { get; set; }

        public IEnumerable<ProjectConstructionLocation> projectConstructionLocations { get; set; }
        public IEnumerable<Project> projects { get; set; }
        public IEnumerable<WorkProgressActivityType> workProgressActivityTypes { get; set; }
        public IEnumerable<WorkProgressActivityDescription> workProgressActivityDescriptions { get; set; }
        public IEnumerable<ActivityWiseDailyProgressDetail> activityWiseDailyProgressDetails { get; set; }
        public IEnumerable<SiteVisitor> siteVisitors { get; set; }
        public IEnumerable<SiteEquipment> siteEquipment { get; set; }
        public IEnumerable<SiteManpower> siteManpowers { get; set; }
        public IEnumerable<ActivityWiseProjectLocationModel> activityWiseProjectLocations { get; set; }
        public IEnumerable<SiteConstraint> siteConstraints { get; set; }
        public IEnumerable<Unit> units { get; set; }
        public DailyProgressReport dailyProgressReport { get; set; }
    }
}
