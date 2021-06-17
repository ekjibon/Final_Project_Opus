using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Data.Entity
{
    [Table("MeetingDocAttachment", Schema = "MMS")]
    public class MeetingDocAttachment:Base
    {
        public int? docId { get; set; }
        public MeetingDocument doc { get; set; }

        public string fileUrl { get; set; }

        public string title { get; set; }
    }
}
