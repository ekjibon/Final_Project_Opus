using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("ProjectLocationActivityDetails", Schema = "SCM")]
    public class ProjectLocationActivityDetails:Base
    {
        public int? projectGridLocationId { get; set; }
        public ProjectGridLocation projectGridLocation { get; set; }
        public int? progressActivityTypeId { get; set; }
        public WorkProgressActivityType progressActivityType { get; set; }
        public int? unitId { get; set; }
        public Unit unit { get; set; }
        public decimal? qty { get; set; }
    }
}
