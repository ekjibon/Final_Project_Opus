using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation
{
    [Table("Offense", Schema = "HR")]
    public class Offense : Base
    {
        public string name { get; set; }

        public string description { get; set; }

    }
}
