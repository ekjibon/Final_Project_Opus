using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("WorkProgressActivityDescriptions", Schema = "SCM")]
    public class WorkProgressActivityDescription:Base
    {
        public int? progressReportId { get; set; }
        public DailyProgressReport progressReport { get; set; }
        public int? progressActivityTypeId { get; set; }
        public WorkProgressActivityType progressActivityType { get; set; }
        public string locationName { get; set; }
        public int? projectConstructionLocationId { get; set; }
        public ProjectConstructionLocation projectConstructionLocation { get; set; }
        public string gridName { get; set; }
        public int? projectGridLocationId { get; set; }
        public ProjectGridLocation projectGridLocation { get; set; }
        public string unit { get; set; }
        public decimal? totalQty { get; set; }
        public string forDayPlanned { get; set; }
        public string forDayAchived { get; set; }
        public string nextDayPlanned { get; set; }
        public string cumAchived { get; set; }
        public string compPercent { get; set; }
        [NotMapped]
        public int? projectLoacationId { get; set; }
        
    }
}
