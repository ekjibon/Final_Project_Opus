using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("JobPost", Schema = "RCRT")]
    public class JobPost : Base
    {
        public int? jobReqId { get; set; }
        public JobRequisition jobReq { get; set; }
        public string media { get; set; }
        public DateTime? postDate { get; set; }
        public DateTime? deadline { get; set; }
    }
}
