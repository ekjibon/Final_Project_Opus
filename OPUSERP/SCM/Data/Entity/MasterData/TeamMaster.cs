using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("TeamMaster", Schema = "SCM")]
    public class TeamMaster:Base
    {
        [StringLength(50)]
        public string teamCode { get; set; }

        [StringLength(250)]
        public string teamName { get; set; }

        public int? leaderId { get; set; }

        public int? isActive { get; set; }

        public int? shortOrder { get; set; }
        [NotMapped]
        public string teamLeader { get; set; }
    }
}
