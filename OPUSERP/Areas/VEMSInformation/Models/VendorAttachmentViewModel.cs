using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSInformation.Models
{
    public class VendorAttachmentViewModel
    {
        public int? registrationFormId { get; set; }

        public int? companyId { get; set; }

        public string fileFor { get; set; }

        public string fileNmae { get; set; }

        public string filePath { get; set; }

        public string fileType { get; set; }

        public string remark { get; set; }

        public IEnumerable<VendorAttachment> vendorAttachments { get; set; }
    }
}
