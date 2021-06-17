using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview
{
    [Table("ExitInterviewPayNBenefits", Schema = "HR")]
    public class ExitInterviewPayNBenefits : Base
    {
        public int? exitInterviewMasterId { get; set; }
        public ExitInterviewMaster exitInterviewMaster { get; set; }

        public int? payAndBenefitsId { get; set; }
        public PayAndBenefits  payAndBenefits { get; set; }

        public string payAndBenefitsText { get; set; }
    }
}
