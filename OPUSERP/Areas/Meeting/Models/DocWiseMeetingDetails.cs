using OPUSERP.Meeting.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Models
{
    public class DocWiseMeetingDetails
    {
        public MeetingDocument meetingDocument { get; set; }
        public IEnumerable<MeetingDetail> meetingDetails { get; set; }
    }
}
