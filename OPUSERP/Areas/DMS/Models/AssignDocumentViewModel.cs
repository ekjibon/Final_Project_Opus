using OPUSERP.DMS.Data.Entity;
using System.Collections.Generic;

namespace OPUSERP.Areas.DMS.Models
{
    public class AssignDocumentViewModel
    {
        public int? employeeInfoId { get; set; }
        public int? assigndocumentMasterId { get; set; }
        public int? documentPhotoAttachmentId { get; set; }
        public int? isViewed { get; set; }
        public string hasEmployee { get; set; }

        public int?[] ids { get; set; }

        public IEnumerable<AssignDocument> assignDocuments { get; set; }
    }
}
