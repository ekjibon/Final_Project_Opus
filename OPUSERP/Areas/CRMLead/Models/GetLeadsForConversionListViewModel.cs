using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class GetLeadsForConversionListViewModel
    {
        public int? Id { get; set; }
        public string leadName { get; set; }
        public string leadNumber { get; set; }
        public string leadOwner { get; set; }
        public string leadShortName { get; set; }
        public string sectorName { get; set; }
        public string ownerShipTypeName { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }



    }
}
