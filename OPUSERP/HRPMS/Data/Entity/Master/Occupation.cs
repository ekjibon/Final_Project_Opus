using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Occupation", Schema = "HR")]
    public class Occupation:Base
    {
        [Required]
        public string occupationName { get; set; }
        public string occupationNameBn { get; set; }

        public string occupationShortName { get; set; }
    }
}
