using OPUSERP.CRM.Data.Entity.Client;
using OPUSERP.CRM.Data.Entity.Lead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.CRMClient.Models
{
    public class OwnerChangeViewModel
    {
        public string changeType { get; set; }
        public string previousOwner { get; set; }
        public string previousOwnerUser { get; set; }
        public string newOwner { get; set; }
        public string newOwnerUser { get; set; }
       
        public string tlasp { get; set; }
      
        public string newOwneremail { get; set; }
        public int?[] leadsId { get; set; }
        public int?[] isactive { get; set; }


        public IEnumerable<Clients>  clients { get; set; }
        public IEnumerable<GetClientInfoListViewModel> getClientInfoListViewModels { get; set; }
        public IEnumerable<LeadsBankInfo> leadsBankInfos { get; set; }
    }
}
