using OPUSERP.DMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.DMS.Models
{
    public class DocumentCategoryViewModel
    {
        public string documentCategoryId { get; set; }
        public string documentCategoryName { get; set; }

        public IEnumerable<DocumentCategory> documentCategories { get; set; }
    }
}
