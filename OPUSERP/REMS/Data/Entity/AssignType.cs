using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.REMS.Data.Entity
{
    [Table("AssignType", Schema = "REMS")]
    public class AssignType:Base
    {
        public string assignTypeName { get; set; }
    }
}
