using OPUSERP.DMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.DMS.Models
{
    public class FieldSettingViewModel
    {
        public int fieldSettingId { get; set; }
        public int fieldTypeId { get; set; }
        public int documentCategoryId { get; set; }
        public string fieldName { get; set; }

        public IEnumerable<FieldSettings> fieldSettings { get; set; }
        public IEnumerable<FieldType> fieldTypes { get; set; }
        public IEnumerable<DocumentCategory> documentCategories { get; set; }
    }
}
