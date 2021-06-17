using Microsoft.AspNetCore.Http;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OPUSERP.Areas.CRMLead.Models
{
    public class ContactsSpViewModel
    {
        public string leadName { get; set; }
        public string resourceName { get; set; }

 
        public string designationName { get; set; }

       
        public string phone { get; set; }

       
        public string mobile { get; set; }

        public string email { get; set; }
        public int? crmdesignationsId { get; set; }
        public string leadOwner { get; set; }

        
    }
}
