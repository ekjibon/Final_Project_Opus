using OPUSERP.CRO.Data.Entity.DistributeJob;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRO.Models
{
    public class IRCMeetingAttendanceViewModel
    {
        public int ircAttendanceId { get; set; }
        public int? operationMasterId { get; set; }
        public int? employeeInfoId { get; set; }
        public string comments { get; set; }
        public int? agreementId { get; set; }
        public int? approveforcroid { get; set; }
        public string assignTeam { get; set; }
        public string assignTo { get; set; }
        public DateTime? assignDate {get; set; }
        public DateTime? ircDate { get; set; }
        public string ratingTypeName { get; set; }
        public string ratingValue { get; set; }
        public string shortRatingName { get; set; }
        public string outlook { get; set; }      

        public IEnumerable<OperationMaster> operationMasters { get; set; }
        public IEnumerable<RatingValue> ratingValues { get; set; }
        public IEnumerable<PreviousRatingDataViewModel> previousRatingDataViewModels { get; set; }
        public IEnumerable<GetOMByassignToReviewerViewModel> getOMByassignToReviewerViewModels { get; set; }
    }
}
