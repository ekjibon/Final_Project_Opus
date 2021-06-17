using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRO.Models
{
    public class GetLeadInfoInCroViewModel
    {
        public int? Id { get; set; }
        public int? leadId { get; set; }
        public string leadName { get; set; }
        public string leadShortName { get; set; }
        public string leadNumber { get; set; }
        public string agreementTypeName { get; set; }
        public string remarks { get; set; }
        public string agreementNo { get; set; }
        public string agreementCategoryName { get; set; }
        public string agreementOwner { get; set; }
        public string fiTypeName { get; set; }
        //public string ratingTypeName { get; set; }
        //public string ratingYearName { get; set; }
        public string agreementStatusName { get; set; }
        public string ownerShipTypeName { get; set; }
        public string mailAddress { get; set; }
        public string divisionName { get; set; }
        public string districtName { get; set; }
        public string thanaName { get; set; }
        public string leadEmail { get; set; }
        public string leadMobile { get; set; }
        public string typeName { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string bankType { get; set; }
        public string maillingAddress { get; set; }
        //public string contactOwner { get; set; }
        //public string resourceName { get; set; }
        //public string email { get; set; }
        //public string phone { get; set; }
        //public string mobile { get; set; }
        public string comments { get; set; }
        public string assignDateReview { get; set; }
        public string reportDate { get; set; }
        public string assignDate { get; set; }
        public string agreementDate { get; set; }
        public string expiredDate { get; set; }
        public int? jobStatusId { get; set; }
        public int? declaration { get; set; }
        public int? categoryId { get; set; }
        public string sectorName { get; set; }
        public string leaderName { get; set; }
        public string jobStatusName { get; set; }
    }
}
