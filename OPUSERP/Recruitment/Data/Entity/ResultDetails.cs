using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("ResultDetails", Schema = "RCRT")]
    public class ResultDetails : Base
    {
        public int resultId { get; set; }
        public ResultMaster ResultMaster { get; set; }

        public int examTypeId { get; set; }
        public ExamType ExamType { get; set; }

        public int? marks { get; set; }
        public int? percentage { get; set; }
        public string status { get; set; }
    }
}
