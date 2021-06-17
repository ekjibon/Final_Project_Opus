using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Subject", Schema = "HR")]
    public class Subject:Base
    {
        [Required]
        public string subjectName { get; set; }
        public string subjectNameBn { get; set; }

        public string subjectShortName { get; set; }
    }
}
