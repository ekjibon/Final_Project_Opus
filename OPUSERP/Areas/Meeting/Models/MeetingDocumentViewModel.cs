using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Meeting.Data.Entity;
using OPUSERP.VIMS.Data;
using OPUSERP.Workflow.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Models
{
    public class MeetingDocumentViewModel
    {
        public string number { get; set; }

        public string subject { get; set; }

        public string content { get; set; }
        
        public IFormFile[] attachments { get; set; }

        public int? employeeId { get; set; }

        public int?[] ids { get; set; }

        public int docId { get; set; }

        public string[] fileTitle { get; set; }

        public string[] fileSubject { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public int? isClose { get; set; }

        public int? isInitial { get; set; }

        public int? ReviewId { get; set; }

        public string noteType { get; set; }

        public MeetingDocument docs { get; set; }
        public MeetingDocWithSgnature doc { get; set; }
        public IEnumerable<MeetingDocAttachment> docAttachment { get; set; }
        //public IEnumerable<MeetingDocRoute> reviewRoutes { get; set; }
        public IEnumerable<MeetingDocRoute>  docRoutes { get; set; }
        public MeetingDocRoute docCheck { get; set; }
        public IEnumerable<MeetingRouteWithSignatureforDoc>  docRoute { get; set; }

        public IEnumerable<MeetingDocument> createdNote { get; set; }
        public IEnumerable<MeetingDocWithReviewIdModel> docWithReviewIdModels { get; set; }
        public IEnumerable<Company> companies { get; set; }

        public MeetingInfo meetingInfo { get; set; }
        public IEnumerable<MeetingDocs> meetingDocs { get; set; }
        public IEnumerable<MeetingInfo> meetingInfos { get; set; }
        public IEnumerable<MeetingUser> meetingUsers { get; set; }
        public IEnumerable<MeetingDetail> meetingDetails { get; set; }
        public IEnumerable<DocWiseMeetingDetails> docWiseMeetingDetails { get; set; }
        public IEnumerable<MeetingAttendance> meetingAttendancesPresents { get; set; }
        public IEnumerable<MeetingAttendance> meetingAttendancesAbsents { get; set; }
    }
}
