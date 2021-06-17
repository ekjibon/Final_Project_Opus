using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Year", Schema = "HR")]
    public class Year : Base
    {
        public string year { get; set; }

        public string remarks { get; set; }
    }
}
