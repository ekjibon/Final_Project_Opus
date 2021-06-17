using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ProjectActivityByLocationViewModel
    {
        public int? projectLocationId { get; set; }
        public int? projectId { get; set; }
        public string applicationUserId { get; set; }
        public string userName { get; set; }
        public string[] levelLocation { get; set; }
        public string[] location { get; set; }

        public int?[] activity { get; set; }
        public int?[] activityUnit { get; set; }
        public decimal?[] activityQty { get; set; }
        public int?[] locationIndexForActivity { get; set; }
        public int?[] levelIndexForGrid { get; set; }

        public int?[] item { get; set; }
        public int?[] itemUnit { get; set; }
        public decimal?[] itemQty { get; set; }
        public int?[] locationIndexForItem { get; set; }
        public int?[] activityIndexForItem { get; set; }

        public string remarks { get; set; }

        public ProjectConstructionLocation Location { get; set; }
        public IEnumerable<ProjectLocationActivityDetails> Activities { get; set; }
        public IEnumerable<ActivityWiseItemDetial> Items { get; set; }
    }
}
