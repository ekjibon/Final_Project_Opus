using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Cold
{
    [Table("ColdActivityTeams", Schema = "CRM")]
    public class ColdActivityTeams:Base
    {
        public int coldActivityMastersId { get; set; }
        public ColdActivityMasters coldActivityMasters { get; set; }

        public int teamId { get; set; }
        public Team team { get; set; }
    }
}
