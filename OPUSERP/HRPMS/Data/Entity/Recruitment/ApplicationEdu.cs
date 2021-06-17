using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment
{
    [Table("ApplicationEdu", Schema = "HR")]
    public class ApplicationEdu : Base
    {
        public int applicationFormId { get; set; }
        public ApplicationForm applicationForm { get; set; }

        public string degree { get; set; }

        public string institution { get; set; }

        public string boardUniversity { get; set; }

        public string rollNo { get; set; }

        public string country { get; set; }

        public string yearOfCertification { get; set; }

        public string mainSubject { get; set; }

        public string gpaDivision { get; set; }

        [StringLength(20)]
        public string type { get; set; }
    }
}
