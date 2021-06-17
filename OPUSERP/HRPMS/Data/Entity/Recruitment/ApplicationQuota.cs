using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment
{
    [Table("ApplicationQuota", Schema = "HR")]
    public class ApplicationQuota : Base
    {
        public string type { get; set; }
        public string document { get; set; }
        public string relation { get; set; }

        public int applicationFormId { get; set; }
        public ApplicationForm applicationForm { get; set; }
    }
}
