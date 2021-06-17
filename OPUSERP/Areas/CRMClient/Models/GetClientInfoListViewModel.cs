using OPUSERP.CRM.Data.Entity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.CRMClient.Models
{
    public class GetClientInfoListViewModel
    {
        public int? Id { get; set; }
        public int? leadsId { get; set; }
        public string leadName { get; set; }
        public string leadNumber { get; set; }
        public string leadOwner { get; set; }
        public string leadShortName { get; set; }
        public string sectorName { get; set; }
        public string agreementCategoryName { get; set; }
        public string agreementStatusName { get; set; }

        public string ratingCategoryName { get; set; }
        public string bankName { get; set; }
        public string BranchName { get; set; }
        public string AgreementDate { get; set; }
        public string expiredDate { get; set; }
        public string jobStatusName { get; set; }
        public string Validtill { get; set; }
        public int? Ageing { get; set; }
        public string ratingYearName { get; set; }
        public string ratingTypeName { get; set; }
    }

       
}
