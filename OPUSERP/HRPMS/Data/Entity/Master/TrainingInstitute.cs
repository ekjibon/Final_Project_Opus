using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("TrainingInstitute", Schema = "HR")]
    public class TrainingInstitute:Base
    {
        [Required]
        public string trainingInstituteName { get; set; }
        public string trainingInstituteNameBn { get; set; }

        public string trainingInstituteShortName { get; set; }
    }
}
