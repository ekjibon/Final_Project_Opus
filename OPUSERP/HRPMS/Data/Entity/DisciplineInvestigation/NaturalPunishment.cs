using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation
{
    [Table("NaturalPunishment", Schema = "HR")]
    public class NaturalPunishment :  Base
    {
        public string name { get; set; }

        public string description { get; set; }
    }
}
