using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.MasterData
{
    [Table("SignatureType", Schema = "ACCOUNT")]
    public class SignatureType : Base
    {
        [MaxLength(100)]
        public string signatureTypeName { get; set; }
        
    }
}
