using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview
{
    [Table("ExitInterviewFeelAndFollowings", Schema = "HR")]
    public class ExitInterviewFeelAndFollowings : Base
    {
        public int? exitInterviewMasterId { get; set; }
        public ExitInterviewMaster exitInterviewMaster { get; set; }

        public int? feelAboutTheFollowingId { get; set; }
        public FeelAboutTheFollowing  feelAboutTheFollowing { get; set; }

        public string feelAboutFollowinsText { get; set; }
    }
}
