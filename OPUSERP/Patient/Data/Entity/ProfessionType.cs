using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Patient.Data.Entity
{
    [Table("ProfessionType", Schema = "HOSPTL")]
    public class ProfessionType : Base
    {
        [MaxLength(150)]
        public string professionName { get; set; }

        [MaxLength(550)]
        public string professionDescription { get; set; }

    }
}
