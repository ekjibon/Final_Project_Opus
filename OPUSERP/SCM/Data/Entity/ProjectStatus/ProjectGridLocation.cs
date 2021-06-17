using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("ProjectGridLocations", Schema = "SCM")]
    public class ProjectGridLocation:Base
    {
        public int? projectConstructionLocationId { get; set; }
        public ProjectConstructionLocation projectConstructionLocation { get; set; }
        public string gridLocation { get; set; }
        public string remarks { get; set; }
    }
}
