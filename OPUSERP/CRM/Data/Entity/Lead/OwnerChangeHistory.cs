using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("OwnerChangeHistory", Schema = "CRM")]
    public class OwnerChangeHistory : Base
    {
        public int? leadsId { get; set; }
        public Leads  leads { get; set; }

       
        [MaxLength(250)]
        public string peviousOwner { get; set; }
        [MaxLength(250)]
        public string nextOwner { get; set; }
       
        
    }
}
