using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("SiteManpowers", Schema = "SCM")]
    public class SiteManpower:Base
    {
        public int? progressReportId { get; set; }
        public DailyProgressReport progressReport { get; set; }
        public string name { get; set; }
        public string planned { get; set; }
        public string actual { get; set; }
        public string description { get; set; }
        public string unitName { get; set; }
    }
}
