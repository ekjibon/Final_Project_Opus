using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.DMS.Data.Entity
{
    [Table("FieldType", Schema = "Doc")]
    public class FieldType : Base
    {
        [MaxLength(20)]
        public string fieldTypeName { get; set; }
    }
}
