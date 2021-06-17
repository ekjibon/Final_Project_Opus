using OPUSERP.DMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.DMS.Models
{
    public class FieldTypeViewModel
    {
        public string fieldTypeId { get; set; }
        public string fieldTypeName { get; set; }

        public IEnumerable<FieldType> fieldTypes { get; set; }
    }
}
