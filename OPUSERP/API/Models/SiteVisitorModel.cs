using Microsoft.AspNetCore.Http;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.API.Models
{
    public class SiteVisitorModel
    {
        public int? progressReportId { get; set; }
        public DailyProgressReport progressReport { get; set; }
        public string visitorName { get; set; }
        public string visitorOrganization { get; set; }
        public string mobileNo { get; set; }
        public IFormFile imagePath { get; set; }
    }
}
