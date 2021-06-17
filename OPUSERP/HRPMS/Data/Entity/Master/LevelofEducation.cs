using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("LevelofEducation", Schema = "HR")]
    public class LevelofEducation:Base
    {
        [Required]
        public string levelofeducationName { get; set; }
        public string levelofeducationNameBn { get; set; }
    }
}
