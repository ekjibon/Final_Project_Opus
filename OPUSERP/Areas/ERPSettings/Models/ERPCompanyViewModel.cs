using Microsoft.AspNetCore.Http;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.ERPSettings.Models
{
    public class ERPCompanyViewModel
    {
        public int? companyId { get; set; }

        public string companyName { get; set; }

        public string ownerName { get; set; }

        public string managerName { get; set; }

        public string tradeLicense { get; set; }
        
        public string officeTelephone { get; set; }
        
        public int? permanentEmployee { get; set; }

        public string vatNo { get; set; }

        public string tinNo { get; set; }

        public string companyEmail { get; set; }

        public string alternetEmail { get; set; }

        public string loginId { get; set; }

        public string password { get; set; }

        public string fileName { get; set; }

        public string filePath { get; set; }

        public string addressLine { get; set; }

        public IFormFile logo { get; set; }

        public IEnumerable<Company> companies { get; set; }
    }
}
