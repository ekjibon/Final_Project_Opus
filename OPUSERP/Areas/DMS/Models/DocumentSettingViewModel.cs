using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.DMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.DMS.Models
{
    public class DocumentSettingViewModel
    {
        public int documentMasterId { get; set; }
        public int? documentCategoryId { get; set; }
        public int? leadsId { get; set; }
        public int? employeeInfoId { get; set; }
        public string documentName { get; set; }
        public string documentNo { get; set; }        
        public DateTime? docCreateDate { get; set; }       
        public string docCreateBy { get; set; }

        public int?[] headIdAll { get; set; }
        public string[] amountAll { get; set; }

        public string fieldTypeName { get; set; }

        public IEnumerable<DocumentCategory> documentCategories { get; set; }
        public IEnumerable<DocumentMaster> documentMasters { get; set; }
        public IEnumerable<TotalDocumentByLeadViewModel> totalDocumentByLeadViewModels { get; set; }
        public DocumentMaster documentMaster { get; set; }
        public IEnumerable<DocumentDetail> documentDetails { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        //public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<DocumentAccessViewModel> documents { get; set; }
        public IEnumerable<DocumentStatusLog> documentStatusLogs { get; set; }
    }
}
