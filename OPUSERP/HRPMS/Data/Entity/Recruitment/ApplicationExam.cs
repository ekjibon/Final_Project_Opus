using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment
{
    [Table("ApplicationExam", Schema = "HR")]
    public class ApplicationExam : Base
    {
        public string type { get; set; }
        public string totalMark { get; set; }
        public string passMark { get; set; }
    }
}
