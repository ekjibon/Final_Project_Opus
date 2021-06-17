using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.DMS.Data.Entity
{
    [Table("FieldSettings", Schema = "Doc")]
    public class FieldSettings : Base
    {
        public int? fieldTypeId { get; set; }
        public FieldType fieldType { get; set; }

        public int? documentCategoryId { get; set; }
        public DocumentCategory documentCategory { get; set; }
        [MaxLength(300)]
        public string fieldName { get; set; }
    }
}
