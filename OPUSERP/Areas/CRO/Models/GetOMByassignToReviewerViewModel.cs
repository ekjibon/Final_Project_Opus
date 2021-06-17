using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRO.Models
{
    public class GetOMByassignToReviewerViewModel
    {
        public int? Id { get; set; }
        public int? agreementId { get; set; }
        public string leadName { get; set; }
        public string agreementTypeName { get; set; }
        public string remarks { get; set; }
        public string agreementNo { get; set; }
        public string agreementCategoryName { get; set; }
        public string agreementOwner { get; set; }
        public string ratingTypeName { get; set; }
        public string ratingYearName { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string comments { get; set; }
        public string assignDateReview { get; set; }
        public string reportDate { get; set; }
        public string assignDate { get; set; }
        public int? jobStatusId { get; set; }
        public int? declaration { get; set; }
        public int? categoryId { get; set; }
        public string sectorName { get; set; }
        public string leaderName { get; set; }
        public string jobStatusName { get; set; }
    }
}
