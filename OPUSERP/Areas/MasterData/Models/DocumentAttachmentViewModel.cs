using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MasterData.Models
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

        public int? moduleId { get; set; }

        public int? leadsId { get; set; }
        public string leadName { get; set; }

        public virtual IEnumerable<DocumentPhotoAttachment> documentAttachments { get; set; }
    }
}
