using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("ActivityTeam", Schema = "CRM")]
    public class ActivityTeam : Base
    {

        public int? activityMasterId { get; set; }
        public ActivityMaster activityMaster { get; set; }

        public int? teamId { get; set; }
        public Team team { get; set; } 
      


    }
}
