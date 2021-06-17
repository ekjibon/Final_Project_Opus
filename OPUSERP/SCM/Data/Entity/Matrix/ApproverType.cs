using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.Matrix
{
    [Table("ApproverType", Schema = "SCM")]
    public class ApproverType:Base
    {
        public string approverTypeName { get; set; }
        public int? shortOrder { get; set; }
    }
}
