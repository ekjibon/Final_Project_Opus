using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("AgreementStatus", Schema = "CRM")]
    public class AgreementStatus : Base
    {       
        [MaxLength(50)]
        public string agreementStatusName { get; set; }
    }
}
