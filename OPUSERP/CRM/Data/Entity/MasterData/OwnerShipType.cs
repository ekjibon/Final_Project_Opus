using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("OwnerShipType", Schema = "CRM")]
    public class OwnerShipType : Base
    {
        [Required]
        public string ownerShipTypeName { get; set; }
    }
}
