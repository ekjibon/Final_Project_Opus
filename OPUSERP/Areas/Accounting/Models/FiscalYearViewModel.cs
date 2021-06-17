using OPUSERP.Budget.Data.Entity;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class FiscalYearViewModel
    {
        public string yearName { get; set; }
        public string yearCaption { get; set; }
        public string aliasName { get; set; }
        public DateTime? yearStartFrom { get; set; }
        public DateTime? bookStartFrom { get; set; }
        public DateTime? yearEndOn { get; set; }
        public DateTime? lockMonth { get; set; }
        public int? yearStatus { get; set; }
        public int? Id { get; set; }

        public IEnumerable<FiscalYear> fiscalYears { get; set; }
        public FiscalYear fiscalYear { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
    }
}
