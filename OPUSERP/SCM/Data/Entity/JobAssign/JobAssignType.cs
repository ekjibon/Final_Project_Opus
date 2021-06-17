using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.JobAssign
{
    [Table("JobAssignType", Schema = "SCM")]
    public class JobAssignType:Base
    {
        [StringLength(150)]
        public string jobAssignName { get; set; }

        [StringLength(250)]
        public string jobAssignNameBN { get; set; }

        public int? shortOrder { get; set; }
    }
}
