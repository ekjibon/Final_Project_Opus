using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment
{
    [Table("CallForExam", Schema = "HR")]
    public class CallForExam : Base
    {
        public int applicationFormId { get; set; }
        public ApplicationForm applicationForm { get; set; }

        public int? applicationExamId { get; set; }
        public ApplicationExam applicationExam { get; set; }

        public string obtainedMark { get; set; }
        public string status { get; set; }
    }
}
