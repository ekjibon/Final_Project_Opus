using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Data.Entity
{
    [Table("MeetingDocs", Schema = "MMS")]
    public class MeetingDocs:Base
    {
        public int? meetingInfoId { get; set; }
        public MeetingInfo meetingInfo { get; set; }

        public int? docId { get; set; }
        public MeetingDocument doc { get; set; }
    }
}
