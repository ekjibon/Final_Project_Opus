using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("TeamMember", Schema = "SCM")]
    public class TeamMember:Base
    {
        public int? teamMasterId { get; set; }
        public TeamMaster teamMaster { get; set; }

        public int? memberId { get; set; }

        public int? isActive { get; set; }

        public int? shortOrder { get; set; }
    }
}
