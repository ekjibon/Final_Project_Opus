using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("Industry", Schema = "CRM")]
    public class Industry : Base
    {
        [Required]
        public string industryName { get; set; }
    }
}
