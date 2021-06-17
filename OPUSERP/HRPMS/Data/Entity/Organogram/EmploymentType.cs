using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Organogram
{
    [Table("EmploymentType", Schema = "HR")]
    public class EmploymentType : Base
    {
        public string nameEN { get; set; }
        public string nameBN { get; set; }
        public string remarks { get; set; }
    }
}
