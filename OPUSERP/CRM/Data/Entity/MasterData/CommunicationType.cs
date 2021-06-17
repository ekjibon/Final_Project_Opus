using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("CommunicationType", Schema = "CRM")]
    public class CommunicationType : Base
    {
        public int comTypeId { get; set; }       

        [Required]
        public string communicationTypeName { get; set; }
    }
}
