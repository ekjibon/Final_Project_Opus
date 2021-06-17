using OPUSERP.Workflow.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Models
{
    public class MeetingDocWithReviewIdModel
    {
        public Doc doc { get; set; }

        public int? reviewId { get; set; }
    }
}
