using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("AgreementCategory", Schema = "CRM")]
    public class AgreementCategory : Base
    {
       
        [Required]
        public string agreementCategoryName { get; set; }
    }
}
