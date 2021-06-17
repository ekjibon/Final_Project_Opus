using System;

namespace OPUSERP.Areas.CRO.Models
{
    public class MasterJobViewModel
    {
        public int? ID { get; set; }
        public string LeadNumber { get; set; }
        public string agreementNo { get; set; }
        public string LeadName { get; set; }
        public string agreementDate { get; set; }
        public string expiredDate { get; set; }
        public string ratingTypeName { get; set; }
        public string ratingValue { get; set; }
        public string shortRatingName { get; set; }
        public string outlook { get; set; }
        public string AssignTeamLeader { get; set; }
        public string assignto { get; set; }
        public string jobStatusName { get; set; }
        public string leadOwner { get; set; }
        public decimal? ratingAmount { get; set; }
        public string assignTeamDate { get; set; }
        public string reportPrintDate { get; set; }

        public string ratingYearName { get; set; }
        public string agreementTypeName { get; set; }
        public string ratingDate { get; set; }
        public string ratingValidTillDate { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string ratingCategoryName { get; set; }
    

    }
}
