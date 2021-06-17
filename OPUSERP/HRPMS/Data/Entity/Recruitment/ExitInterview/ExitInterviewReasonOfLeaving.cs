using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview
{
    [Table("ExitInterviewReasonOfLeaving", Schema = "HR")]
    public class ExitInterviewReasonOfLeaving : Base
    {
        public int? exitInterviewMasterId { get; set; }
        public ExitInterviewMaster exitInterviewMaster { get; set; }

        public int? reasonForLeavingId { get; set; }
        public ReasonForLeaving  reasonForLeaving { get; set; }

    }
}
