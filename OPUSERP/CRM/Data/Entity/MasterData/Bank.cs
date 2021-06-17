using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("Bank", Schema = "CRM")]
    public class Bank : Base
    {      
        [MaxLength(250)]
        public string bankName { get; set; }

        public int? fiTypeId { get; set; }
        public FIType fiType { get; set; }
    }
}
