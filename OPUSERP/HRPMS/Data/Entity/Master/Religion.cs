using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Religion", Schema = "HR")]
    public class Religion:Base
    {
        [Required]
        public string name { get; set; }
        public string nameBn { get; set; }

        public string shortName { get; set; }
    }
}
