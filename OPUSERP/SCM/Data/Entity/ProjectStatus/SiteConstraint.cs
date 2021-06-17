using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("SiteConstraints", Schema = "SCM")]
    public class SiteConstraint:Base
    {
        public int? progressReportId { get; set; }
        public DailyProgressReport progressReport { get; set; }
        public string name { get; set; }
        public int? statusId { get; set; }
    }
}
