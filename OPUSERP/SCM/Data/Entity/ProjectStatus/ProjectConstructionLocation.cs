using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("ProjectConstructionLocations", Schema = "SCM")]
    public class ProjectConstructionLocation:Base
    {
        public int? projectId { get; set; }
        public Project project { get; set; }
        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string locationPosition { get; set; }
        public string floorNo { get; set; }
        public string remarks { get; set; }
    }
}
