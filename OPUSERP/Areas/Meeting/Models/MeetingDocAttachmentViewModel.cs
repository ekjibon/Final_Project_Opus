using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Models
{
    public class MeetingDocAttachmentViewModel
    {
        public IFormFile[] attachments { get; set; }

        public int docId { get; set; }

        public string[] fileTitle { get; set; }

        public string[] fileSubject { get; set; }
    }
}
