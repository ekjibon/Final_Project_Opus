using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Cadre", Schema = "HR")]
    public class Cadre:Base
    {
        [Required]
        public string cadreName { get; set; }
        public string cadreNameBn { get; set; }

        public string cadreShortName { get; set; }
    }
}
