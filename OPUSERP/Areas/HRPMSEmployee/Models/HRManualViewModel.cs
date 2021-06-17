using Microsoft.AspNetCore.Http;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class HRManualViewModel
    {
        public int? hrManualId { get; set; }
        public IFormFile fileUrl { get; set; }

        public string fileTitle { get; set; }

        public string remarks { get; set; }

        public DateTime? date { get; set; }
        public IEnumerable<HRManualAttachment> hRManualAttachments { get; set; }
    }
}
