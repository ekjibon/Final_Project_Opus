using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("SiteVisitors", Schema = "SCM")]
    public class SiteVisitor:Base
    {
        public int? progressReportId { get; set; }
        public DailyProgressReport progressReport { get; set; }
        public string visitorName { get; set; }
        public string visitorOrganization { get; set; }
        public string mobileNo { get; set; }
        public string imagePath { get; set; }
    }
}
