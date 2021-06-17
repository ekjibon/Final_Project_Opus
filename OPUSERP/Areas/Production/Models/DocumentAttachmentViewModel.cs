using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Areas.Production.Models
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

        public virtual IEnumerable<ProductionDocumentAttachment> documentAttachments { get; set; }
    }
}
