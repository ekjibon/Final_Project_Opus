using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Disciplinary
{
    [Table("NamturalPunishment", Schema = "HR")]
    public class NamturalPunishment:Base
    {
        public string punishment { get; set; }

        public string description { get; set; }
    }
}
