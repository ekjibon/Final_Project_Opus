using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("ActivityWiseItemDetials", Schema = "SCM")]
    public class ActivityWiseItemDetial:Base
    {
        public int? activityDetailsId { get; set; }
        public ProjectLocationActivityDetails activityDetails { get; set; }
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }
        public int? unitId { get; set; }
        public Unit unit { get; set; }
        public decimal? qty { get; set; }
    }
}