using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("LeadsHistory", Schema = "CRM")]
    public class LeadsHistory : Base
    {
        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        [MaxLength(250)]
        public string actionName { get; set; }

        public string actionDetails { get; set; }
    }
}
