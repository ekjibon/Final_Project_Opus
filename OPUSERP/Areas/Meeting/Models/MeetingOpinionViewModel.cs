using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Models
{
    public class MeetingOpinionViewModel
    {
        public int? meetingId { get; set; }
        public int? docId { get; set; }
        public int?[] user { get; set; }
        public string[] comment { get; set; }
        public string summery { get; set; }
        public string decision { get; set; }
    }
}
