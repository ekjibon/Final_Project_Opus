using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("FIType", Schema = "CRM")]
    public class FIType : Base
    {      
        [MaxLength(250)]
        public string fiTypeName { get; set; }       
    }
}
