using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.Data.Entity.AttachmentComment;

namespace OPUSERP.Areas.VMS.Models
{
    public class DocumentAttachmentViewModel
    {
        public int? documentId { get; set; }

        public string actionType { get; set; }

        public int? actionTypeId { get; set; }

        public string documentType { get; set; }

        public string fileName { get; set; }

        public string subject { get; set; }

        public string description { get; set; }

        public virtual IEnumerable<DocumentPhotoAttachment> documentAttachments { get; set; }
    }
}
