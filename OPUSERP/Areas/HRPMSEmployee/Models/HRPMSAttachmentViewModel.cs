using Microsoft.AspNetCore.Http;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class HRPMSAttachmentViewModel
    {
        //Foreign Reliation
        public int employeeId { get; set; }

        public int? atttachmentCategoryId { get; set; }

        public IFormFile fileUrl { get; set; }

        public string fileTitle { get; set; }

        public string employeeNameCode { get; set; }

        public string remarks { get; set; }
        public DateTime? attachmentDate { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<AtttachmentGroup> atttachmentGroups { get; set; }
        public IEnumerable<AtttachmentCategory> atttachmentCategory { get; set; }
        public IEnumerable<HRPMSAttachment> hRPMSAttachments { get; set; }

    }
}
