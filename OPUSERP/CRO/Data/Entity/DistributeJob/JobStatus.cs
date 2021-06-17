using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("JobStatus", Schema = "CRO")]
    public class JobStatus : Base
    {
        [MaxLength(350)]
        public string jobStatusName { get; set; }
        [MaxLength(3)]
        public string isMyJob { get; set; }
        [MaxLength(3)]
        public string isReviewReport { get; set; }
        [MaxLength(3)]
        public string isConversion { get; set; }
    }
}
